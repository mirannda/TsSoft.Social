namespace TsSoft.Social.Samples.Controllers
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Web.Mvc;
    using TsSoft.Social.Facebook;
    using TsSoft.Social.Samples.Models;
    using TsSoft.Social.Vk;

    /// <author>Pavel Kurdikov</author>
    public class HomeController : Controller
    {
        private SocialContext context = new SocialContext();
        private const string VkName = "vk";
        private const string FacebookName = "facebook";
        private const string TwitterName = "twitter";
        private static readonly UrlHelper urlHelper = new UrlHelper(System.Web.HttpContext.Current.Request.RequestContext);

        private VkAuthorized vk = new VkAuthorized(
            ConfigurationManager.AppSettings["Vk.appId"],
            ConfigurationManager.AppSettings["Vk.appSecret"],
            ConfigurationManager.AppSettings["AppBaseUrl"] + urlHelper.Content(urlHelper.Action("VkAuthResponse"))
            );

        private FacebookAuthorization facebook = new FacebookAuthorization(
            ConfigurationManager.AppSettings["Facebook.appId"],
            ConfigurationManager.AppSettings["Facebook.appSecret"],
            ConfigurationManager.AppSettings["AppBaseUrl"] + urlHelper.Content(urlHelper.Action("FacebookAuthResponse"))
            );

        public ActionResult Index()
        {
            return RedirectToAction("SendMessage");
        }

        public ActionResult SendMessage(string message = null)
        {
            if (message != null)
            {
                ViewBag.message = message;
            }
            return View(context);
        }

        public ActionResult Authorization(string message = null)
        {
            ViewBag.message = message;
            return View();
        }

        public RedirectResult VkAuth()
        {
            vk.AppendRight(new[] { VkRight.Offline, VkRight.Notes });
            return Redirect(vk.GetCodeRedirectString());
        }

        public ActionResult VkAuthResponse(string code)
        {
            VkUser user;
            try
            {
                user = vk.GetUser(code);
            }
            catch (Exception e)
            {
                return RedirectToAction("SendMessage", new { message = e.Message });
            }
            if (context.VkUser.Count() != 0)
            {
                context.VkUser.Remove(context.VkUser.First());
            }
            context.VkUser.Add(user);
            context.SaveChanges();
            return RedirectToAction("SendMessage");
        }

        public RedirectResult FacebookAuth()
        {
            string state = Guid.NewGuid().ToString("N");
            Session.Add("state", state);
            facebook.AppendRight(new[] { FacebookRight.PublishActions, FacebookRight.PublishStream, FacebookRight.ManagePages });
            return Redirect(facebook.GetRedirectString(state));
        }

        public ActionResult FacebookAuthResponse(string state, string code, string access_token, string error_reason)
        {
            FacebookUser user;
            try
            {
                user = facebook.GetUser(code);
            }
            catch (Exception e)
            {
                return RedirectToAction("SendMessage", new { message = e.Message });
            }
            if (context.FacebookUser.Count() != 0)
            {
                context.FacebookUser.Remove(context.FacebookUser.First());
            }
            context.FacebookUser.Add(user);
            context.SaveChanges();
            return RedirectToAction("SendMessage");
        }

        public ActionResult ClearAuthorization()
        {
            if (context.FacebookUser.Any())
            {
                context.FacebookUser.Remove(context.FacebookUser.First());
            }
            if (context.VkUser.Any())
            {
                context.VkUser.Remove(context.VkUser.First());
            }
            context.SaveChanges();
            return RedirectToAction("SendMessage");
        }

        [HttpPost]
        public JsonResult MessageRequest(string message, string title, string socialName)
        {
            switch (socialName)
            {
                case VkName:
                    return VkRequest(message, title);

                case FacebookName:
                    return FacebookRequest(message);

                case TwitterName:
                    return Json("");

                default:
                    return Json("");
            }
        }

        private JsonResult VkRequest(string message, string title)
        {
            if (context.VkUser.Count() == 0)
            {
                return Json(new { status = "error", message = "vk.com error: " + "You must be authorized" });
            }
            var publisher = new VkPublisher();
            publisher.User = context.VkUser.First();
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    title = TsSoft.Commons.Text.Formatter.ShortenTextByWord(message, 20);
                }
                var response = publisher.Publish(title, message);
            }
            catch (Exception e)
            {
                return Json(new { status = "error", message = "vk.com error: " + e.Message });
            }
            return Json(new { status = "success", message = "vk.com message sent" });
        }

        private JsonResult FacebookRequest(string message)
        {
            var publisher = new FacebookPublisher();
            if (context.FacebookUser.Count() == 0)
            {
                return Json(new { status = "error", message = "Facebook error: " + "You must be authorized" });
            }
            publisher.User = context.FacebookUser.First();
            if (publisher.User.ExpireTime < DateTime.Now)
            {
                context.FacebookUser.Remove(publisher.User);
                context.SaveChanges();
                return Json(new { status = "error", message = "Facebook error: " + "Токен истек. Авторизуйтесь заново" });
            }
            try
            {
                var response = publisher.Publish(message);
            }
            catch (Exception e)
            {
                return Json(new { status = "error", message = "Facebook error: " + e.Message });
            }
            return Json(new { status = "success", message = "facebook message sent" });
        }
    }
}
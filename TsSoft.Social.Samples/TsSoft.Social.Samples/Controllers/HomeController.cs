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
            return View();
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
            context.FacebookUser.Add(user);
            context.SaveChanges();
            return RedirectToAction("SendMessage");
        }

        [HttpPost]
        public ActionResult Request(string message, string title, string socialName)
        {
            switch (socialName)
            {
                case VkName:
                    return RedirectToAction("VkRequest", new { message = message, title = title });

                case FacebookName:
                    return RedirectToAction("FacebookRequest", new { message = message });

                case TwitterName:
                    return RedirectToAction("SendMessage");

                default:
                    return RedirectToAction("SendMessage");
            }
        }

        public ActionResult VkRequest(string message, string title)
        {
            if (context.VkUser.Count() == 0)
            {
                return RedirectToAction("Authorization", new { message = "Сначала нужно авторизоваться." });
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
                return RedirectToAction("Response", new { error = e.Message });
            }
            return RedirectToAction("Response");
        }

        public ActionResult FacebookRequest(string message)
        {
            var publisher = new FacebookPublisher();
            if (context.FacebookUser.Count() == 0)
            {
                return RedirectToAction("Authorization", new { message = "Сначала нужно авторизоваться." });
            }
            publisher.User = context.FacebookUser.First();
            if (publisher.User.ExpireTime < DateTime.Now)
            {
                context.FacebookUser.Remove(publisher.User);
                context.SaveChanges();
                return RedirectToAction("Authorization", new { message = "Токен истек. Авторизуйтесь заново." });
            }
            try
            {
                var response = publisher.Publish(message);
            }
            catch (Exception e)
            {
                return RedirectToAction("Response", new { error = e.Message });
            }
            return RedirectToAction("Response");
        }

        public ActionResult Response(string error = null)
        {
            ViewBag.error = error;
            return View();
        }
    }
}
namespace TsSoft.Social.Vk
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Helpers = TsSoft.Social.Helpers;

    /// <author>Pavel Kurdikov</author>
    public class VkRegister
    {
        private string appId;
        private string appSecret;
        private string redirectCallbackUrl;
        private ICollection<VkRight> rights;

        protected Helpers.UriBuilder requestBuilder;

        public VkRegister(string appId, string appSecret, string callbackURL)
        {
            this.appId = appId;
            this.appSecret = appSecret;
            redirectCallbackUrl = callbackURL;
            rights = new List<VkRight>();
        }

        public string GetCodeRedirectString()
        {
            return string.Format(VkConst.UriAuthorizeTemplate, appId, string.Join(",", rights.Distinct()), redirectCallbackUrl);
        }

        public VkUser GetUser(string appCode)
        {
            var requestString = string.Format(VkConst.UriGetTokenTemplate, appId, appSecret, appCode, redirectCallbackUrl);
            string response;

            WebClient webClient = new WebClient();
            response = webClient.DownloadString(requestString);
            JObject jsonResponse = JObject.Parse(response);
            var properties = jsonResponse.Children().Cast<JProperty>();
            if (properties.Any(x => x.Name == VkConst.JsonError))
            {
                var message = properties.Single(x => x.Name == VkConst.JsonError).Value.ToString();
                throw new Exception(message);
            }

            var token = properties.Single(x => x.Name == VkConst.JsonToken).Value.ToString();
            var userId = properties.Single(x => x.Name == VkConst.JsonUserId).Value.ToString();
            return new VkUser() { AccessToken = token, UserId = userId };
        }

        public VkRegister AppendRight(VkRight right)
        {
            rights.Add(right);
            return this;
        }

        public VkRegister AppendRight(IEnumerable<VkRight> appendRights)
        {
            foreach (var right in appendRights)
            {
                rights.Add(right);
            }
            return this;
        }
    }
}
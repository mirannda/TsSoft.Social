namespace TsSoft.Social.Vk
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <author>Pavel Kurdikov</author>
    public class VkAuthorized
    {
        private string appId;
        private string appSecret;
        private string redirectCallbackUrl;
        private ICollection<VkRight> rights;

        //protected Helpers.UriBuilder requestBuilder;

        public VkAuthorized(string appId, string appSecret, string callbackURL)
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
            var request = new VkRequest(VkConst.BaseUriGetUser);
            request.Parameters.Add("client_id", appId);
            request.Parameters.Add("client_secret", appSecret);
            request.Parameters.Add("code", appCode);
            request.Parameters.Add("redirect_uri", redirectCallbackUrl);
            
            var response = request.Execute("access_token");
            var properties = response.Children().Cast<JProperty>();
            var token = properties.Single(x => x.Name == VkConst.JsonToken).Value.ToString();
            var userId = properties.Single(x => x.Name == VkConst.JsonUserId).Value.ToString();
            return new VkUser() { AccessToken = token, UserId = userId };
        }

        public VkAuthorized AppendRight(VkRight right)
        {
            rights.Add(right);
            return this;
        }

        public VkAuthorized AppendRight(IEnumerable<VkRight> appendRights)
        {
            foreach (var right in appendRights)
            {
                rights.Add(right);
            }
            return this;
        }
    }
}
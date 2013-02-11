namespace TsSoft.Social.Facebook
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TsSoft.Commons.Collections;

    /// <author>Pavel Kurdikov</author>
    public class FacebookAuthorization
    {
        private string appId;
        private string appSecret;
        private string redirectCallbackUrl;
        private ICollection<FacebookRight> rights;

        public FacebookAuthorization(string appId, string appSecret, string callbackURL)
        {
            this.appId = appId;
            this.appSecret = appSecret;
            redirectCallbackUrl = callbackURL;
            rights = new List<FacebookRight>();
        }

        /// <param name="state">
        ///     A unique string used to maintain application state between the request and callback.
        ///     When Facebook redirects the user back to your redirect_uri, this parameter's value will be included in the response.
        ///     You should use this to protect against Cross-Site Request Forgery.
        /// </param>
        public string GetRedirectString(string state)
        {
            var requestBuilder = new Helpers.UriBuilder();
            requestBuilder.BaseUrl = FacebookConst.BaseUriAuthorize;
            requestBuilder.Append("client_id", appId);
            requestBuilder.Append("state", state);
            requestBuilder.Append("redirect_uri", redirectCallbackUrl);
            requestBuilder.Append("scope", string.Join(",", rights.Distinct().Select(x => Enums.GetEnumDescription(x)).ToList()));
            return requestBuilder.Uri;
        }

        public FacebookUser GetUser(string appCode)
        {
            var request = new FacebookRequest(FacebookConst.BaseUriGetUser);
            request.Parameters.Add("client_id", appId);
            request.Parameters.Add("client_secret", appSecret);
            request.Parameters.Add("redirect_uri", redirectCallbackUrl);
            request.Parameters.Add("code", appCode);

            string response = request.Execute(FacebookMethod.AccessToken);
            string[] fbParameters = response.Split('&');

            var accessToken = fbParameters.Single(x => x.StartsWith(FacebookConst.ResponseAccessToken)).Split('=').Last();
            var expires = Convert.ToInt32(fbParameters.Single(x => x.StartsWith(FacebookConst.ResponseExpireTime)).Split('=').Last());
            return new FacebookUser()
            {
                AccessToken = accessToken,
                ExpireTime = DateTime.Now + TimeSpan.FromMilliseconds(expires),
                UserId = "me",
            };
        }

        public FacebookAuthorization AppendRight(FacebookRight right)
        {
            rights.Add(right);
            return this;
        }

        public FacebookAuthorization AppendRight(IEnumerable<FacebookRight> appendRights)
        {
            foreach (var right in appendRights)
            {
                rights.Add(right);
            }
            return this;
        }
    }
}
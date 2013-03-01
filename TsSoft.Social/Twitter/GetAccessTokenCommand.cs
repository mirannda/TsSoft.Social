namespace TsSoft.Social.Twitter
{
    using System;
    using System.Text.RegularExpressions;
    using TsSoft.Social.Api;
    using TsSoft.Social.OAuth;

    /// <author>Evgeniy Yaroslavov</author>
    public class GetAccessTokenCommand : TwitterRequest, IGetAccessTokenCommand
    {
        public RequestCredentials RequestTokens { get; set; }

        public AccessCredentials Execute()
        {
            UriBuilder.BaseUrl = "https://api.twitter.com/oauth/access_token";
            Parameters.Add("oauth_token", RequestTokens.Token);
            if (string.IsNullOrEmpty(RequestTokens.Verifier))
            {
                Parameters.Add("oauth_verifier", RequestTokens.Verifier);
            }
            Sign();
            string responseString = GetResponse();
            AccessCredentials response = new AccessCredentials();
            response.AccessToken = Regex.Match(responseString, @"oauth_token=([^&]+)").Groups[1].Value;
            response.AccessTokenSecret = Regex.Match(responseString, @"oauth_token_secret=([^&]+)").Groups[1].Value;

            return response;
        }
    }
}

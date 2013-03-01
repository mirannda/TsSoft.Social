namespace TsSoft.Social.Twitter
{
    using System;
    using TsSoft.Social.Api;
    using TsSoft.Social.OAuth;

    /// <summary>
    /// Authorization command for an OAuth Redirection-Based Authorization
    /// </summary>
    /// <author>Evgeniy Yaroslavov</author>
    public class AuthorizeCommand: TwitterRequest, IAuthorizeCommand
    {
        public RequestCredentials RequestTokens { get; set; }

        public string Execute()
        {
            UriBuilder.BaseUrl = "https://api.twitter.com/oauth/authorize";
            UriBuilder.Append("oauth_token", RequestTokens.Token);
            return UriBuilder.Uri;
        }
    }
}

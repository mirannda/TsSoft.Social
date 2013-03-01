namespace TsSoft.Social.Twitter
{
    using System;
    using TsSoft.Social.Api;
    using TsSoft.Social.OAuth;

    /// <author>Evgeniy Yaroslavov</author>
    public class GetRequestTokenCommand : TwitterRequest, IGetRequestTokenCommand
    {
        public string CallbackUrl { get; set; }

        public RequestCredentials Execute()
        {
            UriBuilder.BaseUrl = "https://api.twitter.com/oauth/request_token";
            Parameters.Add("oauth_callback", CallbackUrl);
            Sign();
            string response = GetResponse();
            return new RequestCredentials
            {
                Token = ParseParameter("oauth_token", response),
                TokenSecret = ParseParameter("oauth_token_secret", response),
                Verifier = ParseParameter("oauth_verifier", response)
            };
        }
    }
}

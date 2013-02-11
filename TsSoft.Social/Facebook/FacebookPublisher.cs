namespace TsSoft.Social.Facebook
{
    using System;

    /// <author>Pavel Kurdikov</author>
    public class FacebookPublisher
    {
        public FacebookUser User { get; set; }

        public string Publish(string message)
        {
            var request = new FacebookRequest(String.Format(FacebookConst.UriMethodExecuteTemplate, User.UserId));
            request.AccessToken = User.AccessToken;
            request.Parameters.Add("message", message);
            return request.Execute(FacebookMethod.Feed);
        }
    }
}
namespace TsSoft.Social.Facebook
{
    using System;

    
    public class FacebookPublisher
    {
        /// <summary>
        /// Идентификатор пользователя в Facebook
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Токен доступа для публикации сообщений на стене
        /// </summary>
        public string AccessToken { get; set; }

        public string Publish(string message)
        {
            var request = new FacebookRequest(String.Format("https://graph.facebook.com/{0}/", UserId));
            request.AccessToken = AccessToken;
            request.Parameters.Add("message", message);
            return request.Execute("feed");
        }
    }
}
namespace TsSoft.Social.Twitter
{
    using System;
    using TsSoft.Social.Api;

    /// <author>Evgeniy Yaroslavov</author>
    public class SendMessageCommand: TwitterRequest, ISocialCommand<string>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }

        public string Execute()
        {
            UriBuilder.BaseUrl = "https://api.twitter.com/1.1/statuses/update.json";
            Parameters.Add("status", Text);
            Sign();
            return GetResponse();
        }
    }
}

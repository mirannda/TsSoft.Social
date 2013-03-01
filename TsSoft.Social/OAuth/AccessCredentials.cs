namespace TsSoft.Social.OAuth
{
    /// <summary>
    /// Authenticated request credentials
    /// </summary>
    /// <author>Evgeniy Yaroslavov</author>
    public class AccessCredentials
    {
        public string AccessToken { get; set; }

        public string AccessTokenSecret { get; set; }

        public string ConsumerKey { get; set; }

        public string ConsumerSecret { get; set; }
    }    
}

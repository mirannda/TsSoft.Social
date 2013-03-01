namespace TsSoft.Social.OAuth
{
    /// <summary>
    /// Temporary credentials
    /// </summary>
    /// <author>Evgeniy Yaroslavov</author>
    public class RequestCredentials
    {
        public string Token { get; set; }
        public string TokenSecret { get; set; }
        public string Verifier { get; set; }
    }
}

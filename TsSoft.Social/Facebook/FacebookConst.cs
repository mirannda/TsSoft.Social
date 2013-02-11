namespace TsSoft.Social.Facebook
{
    /// <author>Pavel Kurdikov</author>
    internal static class FacebookConst
    {
        public static string BaseUriAuthorize = "https://www.facebook.com/dialog/oauth";
        public static string BaseUriGetUser = "https://graph.facebook.com/oauth/";
        public static string UriMethodExecuteTemplate = "https://graph.facebook.com/{0}/";

        public static string JsonError = "error";
    }
}
namespace TsSoft.Social.Facebook
{
    /// <author>Pavel Kurdikov</author>
    internal static class FacebookConst
    {
        public static string BaseUriAuthorize = "https://www.facebook.com/dialog/oauth";
        public static string BaseUriGetUser = "https://graph.facebook.com/oauth/";
        public static string UriMethodExecuteTemplate = "https://graph.facebook.com/{0}/";

        public static string ResponseAccessToken = "access_token";
        public static string ResponseExpireTime = "expires";

        public static string JsonError = "error";
        public static string JsonErrorMessage = "message";
        public static string JsonErrorType = "type";
        public static string JsonErrorCode = "code";
    }
}
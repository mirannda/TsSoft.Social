namespace TsSoft.Social.Vk
{
    internal static class VkConst
    {
        public const string UriAuthorizeTemplate = "http://api.vkontakte.ru/oauth/authorize?client_id={0}&scope={1}&redirect_uri={2}";
        public const string BaseUriGetUser = "https://api.vkontakte.ru/oauth";
        public const string MethodExecuteBaseUri = "https://api.vkontakte.ru/method";
        public const string JsonError = "error";
        public const string JsonErrorDescription = "error_description";
        public const string JsonToken = "access_token";
        public const string JsonUserId = "user_id";
    }
}
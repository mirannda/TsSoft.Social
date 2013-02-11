namespace TsSoft.Social.Vk
{
    internal static class VkConst
    {
        /// <summary>
        /// Получение кода авторизации
        /// </summary>
        public const string UriAuthorizeTemplate = "http://api.vkontakte.ru/oauth/authorize?client_id={0}&scope={1}&redirect_uri={2}";

        /// <summary>
        /// Получение токена
        /// </summary>
        public const string UriGetTokenTemplate = "https://api.vkontakte.ru/oauth/access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri={3}";

        /// <summary>
        /// Выполнение метода
        /// </summary>
        public const string MethodExecuteBaseUri = "https://api.vkontakte.ru/method";

        public const string JsonError = "error";
        public const string JsonToken = "access_token";
        public const string JsonUserId = "user_id";
    }
}
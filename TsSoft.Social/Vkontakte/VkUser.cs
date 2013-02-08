namespace TsSoft.Social.Vkontakte
{
    public class VkUser
    {
        /// <summary>
        /// Идентификатор пользователя в Vkontakte
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Токен Vkontakte
        /// </summary>
        public string AccessToken { get; set; }
    }
}
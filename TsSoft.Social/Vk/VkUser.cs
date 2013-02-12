namespace TsSoft.Social.Vk
{
    using System;

    /// <summary>
    /// Vk.com User
    /// </summary>
    /// <author>Pavel Kurdikov</author>
    public class VkUser
    {
        public string VkUserId { get; set; }

        public string AccessToken { get; set; }

        // TODO Контакт всегда возвращает ноль, возможно, в запросе на
        // получение токена чего-то не хватает
        //public DateTime ExpireTime { get; set; }
    }
}
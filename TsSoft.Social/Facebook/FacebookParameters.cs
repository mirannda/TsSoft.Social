namespace TsSoft.Social.Facebook
{
    using System;

    /// <summary>
    /// Параметры для возможности постинга сообщений в Facebook
    /// </summary>
    public class FacebookParameters
    {
        /// <summary>
        /// Идентификатор пользователя в FaceBook
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Код доступа к Facebook
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Время истечения доступа
        /// </summary>
        public DateTime ExpireTime { get; set; }
    }
}
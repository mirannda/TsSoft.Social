namespace TsSoft.Social.Vk
{
    /// <summary>
    /// Возможные права приложения
    /// http://vk.com/developers.php?oid=-1&p=%D0%9F%D1%80%D0%B0%D0%B2%D0%B0_%D0%B4%D0%BE%D1%81%D1%82%D1%83%D0%BF%D0%B0_%D0%BF%D1%80%D0%B8%D0%BB%D0%BE%D0%B6%D0%B5%D0%BD%D0%B8%D0%B9
    /// </summary>
    public enum VkRight
    {
        /// <summary>
        /// Пользователь разрешил отправлять ему уведомления
        /// </summary>
        Notify,

        /// <summary>
        /// Доступ к друзьям
        /// </summary>
        Friends,

        /// <summary>
        /// Доступ к фотографиям
        /// </summary>
        Photos,

        /// <summary>
        /// Доступ к аудиозаписям
        /// </summary>
        Audio,

        /// <summary>
        /// Доступ к видеозаписям
        /// </summary>
        Video,

        /// <summary>
        /// Доступ к документам
        /// </summary>
        Docs,

        /// <summary>
        /// Доступ заметкам пользователя
        /// </summary>
        Notes,

        /// <summary>
        /// Доступ к wiki-страницам
        /// </summary>
        Pages,

        /// <summary>
        /// Доступ к статусу пользователя
        /// </summary>
        Status,

        /// <summary>
        /// Доступ к обычным и расширенным методам работы со стеной.
        ///Внимание, данное право доступа недоступно для сайтов (игнорируется при попытке авторизации)
        /// </summary>
        Wall,

        /// <summary>
        /// Доступ к группам пользователя
        /// </summary>
        Groups,

        /// <summary>
        /// (для Standalone-приложений) Доступ к расширенным методам работы с сообщениями
        /// </summary>
        Messages,

        /// <summary>
        /// Доступ к оповещениям об ответах пользователю
        /// </summary>
        Notifications,

        /// <summary>
        /// Доступ к статистике групп и приложений пользователя, администратором которых он является
        /// </summary>
        Stats,

        /// <summary>
        /// Доступ к расширенным методам работы с рекламным API
        /// </summary>
        Ads,

        /// <summary>
        /// Доступ к API в любое время со стороннего сервера
        /// </summary>
        Offline,

        /// <summary>
        /// озможность осуществлять запросы к API без HTTPS.
        /// Внимание, данная возможность находится на этапе тестирования и может быть изменена
        /// </summary>
        NoHttps,
    }
}
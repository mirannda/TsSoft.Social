namespace TsSoft.Social.Vkontakte
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Регистрация вконтакте
    /// </summary>
    public class VkRegister
    {
        private string m_VkAppId;
        private string m_VkAppSecret;
        private string m_Redirect;

        public VkRegister(string vkAppId, string vkAppSecret, string redirectUrl)
        {
            m_VkAppId = vkAppId;
            m_VkAppSecret = vkAppSecret;
            m_Redirect = redirectUrl;
        }

        /// <summary>
        /// Построение URL для получения кода авторизации
        /// </summary>
        /// <param name="redirect"></param>
        /// <param name="rights">Требуемые права</param>
        /// <returns>URL получения кода</returns>
        public string GetCode(string redirect, IEnumerable<string> rights)
        {
            string reqStrTemplate = "http://api.vkontakte.ru/oauth/authorize?client_id={0}&scope={1}&redirect_uri={2}";
            return string.Format(reqStrTemplate, m_VkAppId, string.Join(",", rights), m_Redirect);
        }

        /// <summary>
        /// Получение пользователя вконтакте
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="redirect"></param>
        /// <returns></returns>
        public VkUser GetToken(string Code, string redirect)
        {
            string reqStrTemplate = "https://api.vkontakte.ru/oauth/access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri={3}";
            string reqStr = string.Format(reqStrTemplate, m_VkAppId, m_VkAppSecret, Code, m_Redirect);
            WebClient webClient = new WebClient();
            var response = webClient.DownloadString(reqStr);
            VkJsonTokenResponse jsonResponse = JsonConvert.DeserializeObject<VkJsonTokenResponse>(response);
            return new VkUser() { AccessToken = jsonResponse.access_token, UserId = jsonResponse.user_id };
        }
    }
}
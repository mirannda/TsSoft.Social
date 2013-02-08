namespace TsSoft.Social.Vkontakte
{
    public class VkPublisher : IPublisher
    {
        /// <summary>
        /// Пользователь вконтакте
        /// </summary>
        public VkUser User { get; set; }

        public VkPublisher(VkUser user)
        {
            User = user;
        }

        public string Publish(Message message)
        {
            var request = new VkRequest("https://api.vkontakte.ru/method");
            request.Parameters.Add("owner_id", User.UserId);
            request.Parameters.Add("access_token", User.AccessToken);
            request.Parameters.Add("title", message.Title);
            request.Parameters.Add("text",  message.Text);
            return request.Execute("notes.add");
        }
    }
}
namespace TsSoft.Social.Vk
{
    public class VkPublisher
    {
        public VkUser User { get; set; }

        /// <summary>
        /// Publish Note
        /// </summary>
        public string Publish(string title, string text)
        {
            var request = new VkRequest(VkConst.MethodExecuteBaseUri);
            request.Parameters.Add("owner_id", User.UserId);
            request.Parameters.Add("access_token", User.AccessToken);
            request.Parameters.Add("title", title);
            request.Parameters.Add("text", text);
            return request.Execute("notes.add");
        }
    }
}
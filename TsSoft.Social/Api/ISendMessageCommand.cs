namespace TsSoft.Social.Api
{
    using Newtonsoft.Json.Linq;
    /// <summary>
    /// Defines a send message command
    /// </summary>
    /// <author>Evgeniy Yaroslavov</author>
    public interface ISendMessageCommand: ISocialCommand<JObject>
    {
        string Title { get; set; }
        string Text { get; set; }
        string Image { get; set; }
        string Link { get; set; }
    }
}

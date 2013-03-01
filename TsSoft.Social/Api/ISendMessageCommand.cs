namespace TsSoft.Social.Api
{
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Posting message to the news feed
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

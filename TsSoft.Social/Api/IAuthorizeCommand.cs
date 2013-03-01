namespace TsSoft.Social.Api
{
    using TsSoft.Social.OAuth;

    /// <summary>
    /// Authorization command for an OAuth Redirection-Based Authorization
    /// </summary>
    /// <author>Evgeniy Yaroslavov</author>
    public interface IAuthorizeCommand: ISocialCommand<string>
    {
        RequestCredentials RequestTokens { get; set; }
    }
}

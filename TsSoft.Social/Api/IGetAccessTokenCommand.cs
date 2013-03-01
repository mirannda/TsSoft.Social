namespace TsSoft.Social.Api
{
    using TsSoft.Social.OAuth;

    /// <summary>
    /// Obtains a set of token credentials
    /// </summary>
    /// <author>Evgeniy Yaroslavov</author>
    public interface IGetAccessTokenCommand: ISocialCommand<AccessCredentials>
    {
        RequestCredentials RequestTokens { get; set; }
    }
}

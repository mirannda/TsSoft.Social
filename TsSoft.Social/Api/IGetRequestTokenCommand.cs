namespace TsSoft.Social.Api
{
    using TsSoft.Social.OAuth;

    /// <summary>
    /// Obtains a set of temporary credentials
    /// </summary>
    /// <author>Evgeniy Yaroslavov</author>
    public interface IGetRequestTokenCommand: ISocialCommand<RequestCredentials>
    {
    }
}

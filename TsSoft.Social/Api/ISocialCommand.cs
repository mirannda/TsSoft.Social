namespace TsSoft.Social.Api
{
    /// <typeparam name="T">Command result type</typeparam>
    /// <author>Evgeniy Yaroslavov</author>
    public interface ISocialCommand<T>
    {
        T Execute();
    }
}

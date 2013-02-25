namespace TsSoft.Social.Api
{
    /// <summary>
    /// Defines a social service command
    /// </summary>
    /// <typeparam name="T">Command result type</typeparam>
    /// <author>Evgeniy Yaroslavov</author>
    public interface ISocialCommand<T>
    {
        T Execute();
    }
}

namespace TsSoft.Social.Facebook
{
    using System.ComponentModel;

    /// <author>Pavel Kurdikov</author>
    public enum FacebookMethod
    {
        /// <summary>
        ///
        /// </summary>
        [Description("access_token")]
        AccessToken,

        /// <summary>
        /// Publish a new post on the given profile's feed/wall. Note that this feature will be removed soon.
        /// Arguments for method: message, picture, link, name, caption, description, source, place, tags
        /// </summary>
        [Description("feed")]
        Feed,

        /// <summary>
        /// Publish a note on the given profile
        /// Arguments for method: message, subject
        /// </summary>
        [Description("notes")]
        Notes,

        /// <summary>
        /// Publish a link on the given profile
        /// Arguments for method: link, message, picture, name, caption, description
        /// </summary>
        [Description("links")]
        Links,

        /// <summary>
        /// Create an event
        /// Arguments for method: name, start_time, end_time
        /// </summary>
        [Description("events")]
        Events,

        /// <summary>
        /// Create an album
        /// Arguments for method: name, message
        /// </summary>
        [Description("albums")]
        Albums,
    }
}
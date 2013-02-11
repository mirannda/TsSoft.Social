namespace TsSoft.Social.Facebook
{
    using System.ComponentModel;
    using TsSoft.Commons.Text;

    /// <summary>
    /// http://developers.facebook.com/docs/reference/login/open-graph-permissions/
    /// </summary>
    /// <author>Pavel Kurdikov</author>
    public enum FacebookRight
    {
        /// <summary>
        /// Allows your app to publish to the Open Graph using Built-in Actions, Achievements, Scores, or Custom Actions.
        /// Your app can also publish other activity which is detailed in the Publishing Permissions doc.
        /// Note: The user-prompt for this permission will be displayed in the first screen of the Enhanced
        /// Auth Dialog and cannot be revoked as part of the authentication flow.
        /// However, a user can later revoke this permission in their Account Settings.
        /// If you want to be notified if this happens, you should subscribe to the permissions object within the Realtime API.
        /// </summary>
        [Description("publish_actions")]
        PublishActions,

        /// <summary>
        /// Allows you to retrieve the actions published by all applications using the built-in music.listens action.
        /// </summary>
        [Description("user_actions.music")]
        UserActionMusic,

        /// <summary>
        /// Allows you to retrieve the actions published by all applications using the built-in news.reads action.
        /// </summary>
        [Description("user_actions.news")]
        UserActionNews,

        /// <summary>
        /// Allows you to retrieve the actions published by all applications using the built-in video.watches action.
        /// </summary>
        [Description("user_actions.video")]
        UserActionVideo,

        /// <summary>
        /// Enables your application to retrieve access_tokens for Pages and Applications that the user administrates.
        /// The access tokens can be queried by calling /<user_id>/accounts via the Graph API.
        /// See here for generating long-lived Page access tokens that do not expire after 60 days.
        /// </summary>
        [Description("manage_pages")]
        ManagePages,

        /// <summary>
        /// Enables your app to post content, comments, and likes to a user's stream and to the streams of the user's friends.
        /// This is a superset publishing permission which also includes publish_actions.
        /// However, please note that Facebook recommends a user-initiated sharing model.
        /// Please read the Platform Policies to ensure you understand how to properly use this permission.
        /// Note, you do not need to request the publish_stream permission in order to use the Feed Dialog, the Requests Dialog or the Send Dialog.
        /// </summary>
        [Description("publish_stream")]
        PublishStream,

        /// <summary>
        /// Provides access to any friend lists the user created.
        /// All user's friends are provided as part of basic data,
        /// this extended permission grants access to the lists of friends a user has created,
        /// and should only be requested if your application utilizes lists of friends.
        /// </summary>
        [Description("read_friendlists")]
        ReadFriendlist,

        /// <summary>
        /// Provides read access to the Insights data for pages, applications, and domains the user owns.
        /// </summary>
        [Description("read_insights")]
        ReadInsights,

        /// <summary>
        /// Provides the ability to read from a user's Facebook Inbox.
        /// </summary>
        [Description("read_mailbox")]
        ReadMailbox,

        /// <summary>
        /// Provides read access to the user's friend requests
        /// </summary>
        [Description("read_requests")]
        ReadRequests,

        /// <summary>
        /// Provides access to all the posts in the user's News Feed and enables your application to perform searches against the user's News Feed
        /// </summary>
        [Description("read_stream")]
        ReadStream,

        /// <summary>
        /// Provides applications that integrate with Facebook Chat the ability to log in users.
        /// </summary>
        [Description("xmpp_login")]
        XmppLogin,

        /// <summary>
        /// Provides the ability to manage ads and call the Facebook Ads API on behalf of a user.
        /// </summary>
        [Description("ads_management")]
        AdsManagement,

        /// <summary>
        /// Enables your application to create and modify events on the user's behalf
        /// </summary>
        [Description("create_event")]
        CreateEvent,

        /// <summary>
        /// Enables your app to create and edit the user's friend lists.
        /// </summary>
        [Description("manage_friendlists")]
        ManageFriendlist,

        /// <summary>
        /// Enables your app to read notifications and mark them as read. Intended usage: This permission should be used to let users read and act on their notifications; it should not be used to for the purposes of modeling user behavior or data mining. Apps that misuse this permission may be banned from requesting it.
        /// </summary>
        [Description("manage_notifications")]
        ManageNotifications,

        /// <summary>
        /// Provides access to the user's online/offline presence
        /// </summary>
        [Description("user_online_presence")]
        UserOnlinePresence,

        /// <summary>
        /// Provides access to the user's friend's online/offline presence
        /// </summary>
        [Description("friends_online_presence")]
        FriendsOnlinePresence,

        /// <summary>
        /// Enables your app to perform checkins on behalf of the user.
        /// </summary>
        [Description("publish_checkins")]
        PublishChekins,

        /// <summary>
        /// Enables your application to RSVP to events on the user's behalf
        /// </summary>
        [Description("rsvp_event")]
        RsvpEvent,
    }
}
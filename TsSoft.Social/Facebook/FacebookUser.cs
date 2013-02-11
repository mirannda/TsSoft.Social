﻿namespace TsSoft.Social.Facebook
{
    using System;

    /// <author>Pavel Kurdikov</author>
    public class FacebookUser
    {
        public string UserId { get; set; }

        public string AccessToken { get; set; }

        public DateTime ExpireTime { get; set; }
    }
}
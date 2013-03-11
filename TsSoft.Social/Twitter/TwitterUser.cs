using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TsSoft.Social.OAuth;

namespace TsSoft.Social.Twitter
{
    public class TwitterUser
    {
        public string Id { get; set; }
        public AccessCredentials AccessTokens { get; set; }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TsSoft.Social.Api;
using TsSoft.Social.OAuth;

namespace TsSoft.Social.Twitter
{
    public class TwitterRequest: HttpRequest
    {
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        
        protected virtual void Sign()
        {
            if (!string.IsNullOrEmpty(AccessTokenSecret) && !string.IsNullOrEmpty(AccessToken))
            {

            }
        }

        protected override string GetResponse()
        {
            Sign();
            return base.GetResponse();
        }

    }
}

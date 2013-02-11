using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace TsSoft.Social.Facebook
{
    public class FacebookAuthRequest : FacebookRequest
    {
        public FacebookAuthRequest(string baseUrl) : base(baseUrl) { }

        public string Execute()
        {
            BuildRequestString("oauth");
            return requestBuilder.Uri;
        }
    }
}
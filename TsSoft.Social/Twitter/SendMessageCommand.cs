using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TsSoft.Social.Api;

namespace TsSoft.Social.Twitter
{
    public class SendMessageCommand : TwitterRequest, ISendMessageCommand
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }

        public JObject Execute()
        {
            return JObject.Parse(GetResponse());
        }
    }
}

namespace TsSoft.Social.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;


    [Obsolete("Moved to TsSoft.Social.Api")]
    public class UriBuilder
    {
        public string BaseUrl { get; set; }

        private IDictionary<string, string> parameters = new Dictionary<string, string>();

        public string Uri
        {
            get
            {
                var builder = new StringBuilder(BaseUrl);
                if (parameters.Count > 0)
                {
                    var parameterPairs = parameters.Select(x => string.Format("{0}={1}", x.Key, HttpUtility.UrlEncode(x.Value)));
                    builder.Append("?").Append(string.Join("&", parameterPairs));
                }
                return builder.ToString();
            }
        }

        public UriBuilder Append(string name, string value)
        {
            parameters.Add(name, value);
            return this;
        }

        public UriBuilder Append(IDictionary<string, string> appendParameters)
        {
            foreach (var parameter in appendParameters)
            {
                parameters.Add(parameter);
            }
            return this;
        }
    }
}
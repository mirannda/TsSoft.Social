namespace TsSoft.Social.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;


    public class UriBuilder
    {
        private string requestUrl;
        private int parameterCount;

        public string Url
        {
            get
            {
                return requestUrl;
            }
        }

        public UriBuilder(string baseUrl)
        {
            requestUrl = baseUrl;
        }

        public UriBuilder Append(string name, string value)
        {
            var builder = new StringBuilder(requestUrl);
            string delimiter = "?";
            if (parameterCount != 0)
            {
                delimiter = "&";
            }
            else
            {
                delimiter = "?";
            }
            builder.Append(String.Format("{0}{1}={2}", delimiter, name, HttpUtility.UrlEncode(value)));
            parameterCount++;
            requestUrl = builder.ToString();
            return this;
        }
    }
}
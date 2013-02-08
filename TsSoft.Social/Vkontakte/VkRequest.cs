namespace TsSoft.Social.Vkontakte
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using Helpers = TsSoft.Social.Helpers;

    internal class VkRequest
    {
        protected string apiUrl;

        protected Helpers.UriBuilder requestBuilder;

        public IDictionary<string, string> Parameters { get; set; }

        public VkRequest(string baseUrl)
        {
            apiUrl = baseUrl;
            Parameters = new Dictionary<string, string>();
        }

        protected void BuildRequestString(string methodName)
        {
            requestBuilder = new Helpers.UriBuilder(String.Format("{0}/{1}", apiUrl.Trim('/'), methodName.Trim('/')));
            foreach (var item in Parameters)
            {
                if (!String.IsNullOrWhiteSpace(item.Value))
                {
                    requestBuilder.Append(item.Key, item.Value);
                }
            }
        }

        public virtual string Execute(string methodName)
        {
            BuildRequestString(methodName);
            string result = "";
            try
            {
                var request = HttpWebRequest.Create(requestBuilder.Url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                var response = request.GetResponse();

                using (var responseReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    result = responseReader.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                using (var errorResponse = (HttpWebResponse)wex.Response)
                {
                    using (var errReader = new StreamReader(errorResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        result = errReader.ReadToEnd();
                    }
                }
            }
            return result;
        }
    }
}

namespace TsSoft.Social.Vk
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using Helpers = TsSoft.Social.Helpers;

    /// <author>Pavel Kurdikov</author>
    public class VkRequest
    {
        protected string apiUrl;

        protected Helpers.UriBuilder requestBuilder;

        public IDictionary<string, string> Parameters { get; set; }

        public VkRequest(string baseUrl)
        {
            apiUrl = baseUrl;
            Parameters = new Dictionary<string, string>();
        }

        public virtual string Execute(string methodName)
        {
            requestBuilder = new Helpers.UriBuilder();
            requestBuilder.BaseUrl = String.Format("{0}/{1}", apiUrl.Trim('/'), methodName.Trim('/'));
            requestBuilder.Append(Parameters);

            string result = string.Empty;
            try
            {
                var request = HttpWebRequest.Create(requestBuilder.Uri);
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
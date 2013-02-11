using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Helpers = TsSoft.Social.Helpers;
namespace TsSoft.Social.Facebook
{
    public class FacebookRequest
    {
        protected string apiUrl;

        protected Helpers.UriBuilder requestBuilder;

        public string AccessToken { get; set; }

        public IDictionary<string, string> Parameters { get; set; }

        public FacebookRequest(string baseUrl)
        {
            apiUrl = baseUrl;
            Parameters = new Dictionary<string, string>();
        }

        protected void BuildRequestString(string methodName)
        {
            requestBuilder = new Helpers.UriBuilder();
            requestBuilder.BaseUrl = String.Format("{0}/{1}", apiUrl.Trim('/'), methodName.Trim('/'));
            requestBuilder.Append(Parameters);
            
            if (!String.IsNullOrWhiteSpace(AccessToken))
            {
                requestBuilder.Append("access_token", AccessToken);
            }
        }

        public virtual string Execute(string methodName)
        {
            BuildRequestString(methodName);
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
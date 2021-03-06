﻿namespace TsSoft.Social.Facebook
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using TsSoft.Commons.Collections;

    /// <author>Pavel Kurdikov</author>
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

        public virtual string Execute(FacebookMethod method)
        {
            requestBuilder = new Helpers.UriBuilder();
            requestBuilder.BaseUrl = String.Format("{0}/{1}", apiUrl.Trim('/'), Enums.GetEnumDescription(method));
            requestBuilder.Append(Parameters);

            if (!String.IsNullOrWhiteSpace(AccessToken))
            {
                requestBuilder.Append("access_token", AccessToken);
            }

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
            ErrorParser(result);
            return result;
        }

        private void ErrorParser(string result)
        {
            JObject jError;
            if (TryParseData(result, out jError))
            {
                var error = jError.Children().Cast<JProperty>();
                if (error.Any(x => x.Name == FacebookConst.JsonError))
                {
                    var errorDescription = error.Single(x => x.Name == FacebookConst.JsonError).Value.Children().Cast<JProperty>();
                    var errorMessage = errorDescription.Single(x => x.Name == FacebookConst.JsonErrorMessage).Value.ToString();
                    throw new Exception(errorMessage);
                }
            }
        }

        private bool TryParseData(String data, out JObject result)
        {
            bool parseResult = false;
            JObject json = null;

            try
            {
                json = JObject.Parse(data);
                parseResult = true;
            }
            catch (Exception)
            {
            }

            result = json;
            return parseResult;
        }
    }
}
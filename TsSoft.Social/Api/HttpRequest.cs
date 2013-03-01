namespace TsSoft.Social.Api
{
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Wrapper for the HttpWebRequest class
    /// </summary>
    /// <author>Evgeniy Yaroslavov</author>
    public abstract class HttpRequest
    {
        protected UriBuilder UriBuilder { get; private set; }
        protected Encoding Encoding { get; set; }
        protected WebRequest Current { get; private set; }
        
        public NameValueCollection Headers { get; private set; }
        public string Method { get; set; }

        public HttpRequest()
        {
            UriBuilder = new UriBuilder();
            Headers = new WebHeaderCollection();
            Encoding = Encoding.UTF8;
            Method = "POST";
        }

        protected virtual string GetResponse()
        {
            Current = HttpWebRequest.Create(UriBuilder.Uri);
            Current.Method = Method;
            Current.Headers.Add(Headers);

            string result = string.Empty;

            WebResponse response;
            try
            {
                response = (HttpWebResponse)Current.GetResponse();
            }
            catch (WebException wex)
            {
                response = wex.Response;
            }
            
            using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
    }
}

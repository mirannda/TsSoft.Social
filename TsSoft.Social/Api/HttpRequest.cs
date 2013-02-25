namespace TsSoft.Social.Api
{
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Provides a wrapper for the HttpWebRequest class
    /// </summary>
    /// <author>Evgeniy Yaroslavov</author>
    public abstract class HttpRequest
    {
        private Encoding encoding = Encoding.UTF8;
        private string method = "POST";

        protected UriBuilder UriBuilder { get; private set; }
        protected Encoding Encoding
        {
            get
            {
                return encoding;
            }
            set
            {
                encoding = value;
            }
        }

        public NameValueCollection Headers { get; private set; }
        public string Method
        {
            get
            {
                return method;
            }
            set
            {
                method = value;
            }
        }

        protected WebRequest Current { get; private set; }
        
        public HttpRequest()
        {
            UriBuilder = new UriBuilder();
            Headers = new WebHeaderCollection();
        }

        protected virtual string GetResponse()
        {
            Current = HttpWebRequest.Create(UriBuilder.Uri);
            Current.Method = Method;
            Current.Headers.Add(Headers);

            string result = string.Empty;

            var response = (HttpWebResponse)Current.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream(), encoding))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
    }
}

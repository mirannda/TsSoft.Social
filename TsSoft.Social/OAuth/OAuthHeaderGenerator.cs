namespace TsSoft.Social.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A utility for creating authorization and request signatures for the OAuth protocol.
    /// </summary>
    /// <author>Evgeniy Yaroslavov</author>
    public class OAuthHeaderGenerator
    {
        private static readonly string[] SecretParameters = new[]
                                                                {
                                                                    "oauth_consumer_secret",
                                                                    "oauth_token_secret",
                                                                    "oauth_signature"
                                                                };

        private static readonly string[] HeaderParameters = new[]
                                                          {
                                                              "oauth_version",
                                                              "oauth_nonce",
                                                              "oauth_timestamp",
                                                              "oauth_signature_method",
                                                              "oauth_consumer_key",
                                                              "oauth_token",
                                                              "oauth_verifier"
                                                          };

        public AccessCredentials Tokens { get; set; }
        public Uri RequestUri { get; set; }
        public string RequestMethod { get; set; }
        public IDictionary<string, object> Parameters { get; set; }
       
        public OAuthHeaderGenerator(Uri requestUri, string requestMethod, AccessCredentials tokens)
        {
            Parameters = new Dictionary<string, object>();
            RequestUri = requestUri;
            RequestMethod = requestMethod;
            Tokens = tokens;
        }

        private static string GenerateTimeStamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds, CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture);
        }

        private static string GenerateNonce()
        {
            return new Random()
                .Next(123400, int.MaxValue)
                .ToString("X", CultureInfo.InvariantCulture);
        }

        private static string NormalizeUrl(Uri url)
        {
            string normalizedUrl = string.Format(CultureInfo.InvariantCulture, "{0}://{1}", url.Scheme, url.Host);
            if (!((url.Scheme == "http" && url.Port == 80) || (url.Scheme == "https" && url.Port == 443)))
            {
                normalizedUrl += ":" + url.Port;
            }

            normalizedUrl += url.AbsolutePath;
            return normalizedUrl;
        }

        private void SetupOAuth()
        { 
            Parameters.Add("oauth_version", "1.0");
            Parameters.Add("oauth_nonce", GenerateNonce());
            Parameters.Add("oauth_timestamp", GenerateTimeStamp());
            Parameters.Add("oauth_signature_method", "HMAC-SHA1");
            Parameters.Add("oauth_consumer_key", Tokens.ConsumerKey);
            Parameters.Add("oauth_consumer_secret", Tokens.ConsumerSecret);

            if (!string.IsNullOrEmpty(Tokens.AccessToken))
            {
                this.Parameters.Add("oauth_token", Tokens.AccessToken);
            }

            if (!string.IsNullOrEmpty(Tokens.AccessTokenSecret))
            {
                this.Parameters.Add("oauth_token_secret", Tokens.AccessTokenSecret);
            }

            string signature = GenerateSignature();

            this.Parameters.Add("oauth_signature", signature);
        }

        private string GenerateSignature()
        {
            IEnumerable<KeyValuePair<string, object>> nonSecretParameters;

            nonSecretParameters = (from p in this.Parameters
                                    where (!SecretParameters.Contains(p.Key))
                                    select p);

            string signatureBaseString = string.Format(
                CultureInfo.InvariantCulture,
                "{0}&{1}&{2}",
                RequestMethod.ToString().ToUpper(CultureInfo.InvariantCulture),
                UrlEncode(NormalizeUrl(RequestUri)),
                UrlEncode(nonSecretParameters));

            string key = string.Format(
                CultureInfo.InvariantCulture,
                "{0}&{1}",
                UrlEncode(this.Tokens.ConsumerSecret),
                UrlEncode(this.Tokens.AccessTokenSecret));

            HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] signatureBytes = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(signatureBaseString));
            return Convert.ToBase64String(signatureBytes);
        }

        public static string UrlEncode(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            value = Uri.EscapeDataString(value);

            // UrlEncode escapes with lowercase characters (e.g. %2f) but oAuth needs %2F
            value = Regex.Replace(value, "(%[0-9a-f][0-9a-f])", c => c.Value.ToUpper());

            // these characters are not escaped by UrlEncode() but needed to be escaped
            value = value
                .Replace("(", "%28")
                .Replace(")", "%29")
                .Replace("$", "%24")
                .Replace("!", "%21")
                .Replace("*", "%2A")
                .Replace("'", "%27");

            // these characters are escaped by UrlEncode() but will fail if unescaped!
            value = value.Replace("%7E", "~");

            return value;
        }

        /// <summary>
        /// Encodes a series of key/value pairs for inclusion in a URL querystring.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A string of all the <paramref name="parameters"/> keys and value pairs with the values encoded.</returns>
        private static string UrlEncode(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            StringBuilder parameterString = new StringBuilder();

            var paramsSorted = from p in parameters
                               orderby p.Key, p.Value
                               select p;

            foreach (var item in paramsSorted)
            {
                if (item.Value is string)
                {
                    if (parameterString.Length > 0)
                    {
                        parameterString.Append("&");
                    }

                    parameterString.Append(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "{0}={1}",
                            UrlEncode(item.Key),
                            UrlEncode((string)item.Value)));
                }
            }

            string j = string.Join("&", paramsSorted);

            return UrlEncode(parameterString.ToString());
        }


        /// <summary>
        /// Generates an authorization header
        /// </summary>
        /// <returns>Authorization header string</returns>
        public string Execute()
        {
            SetupOAuth();

            StringBuilder authHeaderBuilder = new StringBuilder();
            authHeaderBuilder.AppendFormat("OAuth realm=\"{0}\"", "Twitter API");
            
            var sortedParameters = from p in this.Parameters
                                   where HeaderParameters.Contains(p.Key)
                                   orderby p.Key, UrlEncode((p.Value is string) ? (string)p.Value : string.Empty)
                                   select p;

            foreach (var item in sortedParameters)
            {
                authHeaderBuilder.AppendFormat(
                    ",{0}=\"{1}\"",
                    UrlEncode(item.Key),
                    UrlEncode(item.Value as string));
            }

            authHeaderBuilder.AppendFormat(",oauth_signature=\"{0}\"", UrlEncode(this.Parameters["oauth_signature"] as string));

            return authHeaderBuilder.ToString();
        }
    }
}

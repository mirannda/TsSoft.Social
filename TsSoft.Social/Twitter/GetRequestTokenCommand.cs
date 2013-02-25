namespace TsSoft.Social.Twitter
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using TsSoft.Social.Api;
    public class GetRequestTokenCommand : TwitterRequest
    {
        public string CallbackUrl { get; set; }

        private IDictionary<string, string> signParameters = new Dictionary<string, string>(); 
        
        protected override void Sign()
        {
           /*  signParameters.Add("oauth_version", "1.0");
            signParameters.Add("oauth_nonce", GenerateNonce());
            signParameters.Add("oauth_timestamp", GenerateTimeStamp());
            signParameters.Add("oauth_signature_method", "HMAC-SHA1");
            signParameters.Add("oauth_consumer_key", Tokens.ConsumerKey);
            signParameters.Add("oauth_consumer_secret", Tokens.ConsumerSecret);

            if (!string.IsNullOrEmpty(Tokens.AccessToken))
            {
                signParameters.Add("oauth_token", Tokens.AccessToken);
            }

            if (!string.IsNullOrEmpty(Tokens.AccessTokenSecret))
            {
                signParameters.Add("oauth_token_secret", Tokens.AccessTokenSecret);
            }

            foreach (var param in signParameters)
            {
                Request.Headers.Add(param.Key, param.Value);
            }
            Request.Headers.Add("oauth_signature", GenerateSignature()); */
        }

        public new void Execute()
        {
            UriBuilder = new Api.UriBuilder("https://api.twitter.com/oauth/request_token");
            UriBuilder.Append("oauth_callback", CallbackUrl);
            Sign();
        }

        /*  private string GenerateSignature()
         {
             string signatureBaseString = string.Format(
                 CultureInfo.InvariantCulture,
                 "{0}&{1}&{2}",
                 Request.Method.ToUpper(CultureInfo.InvariantCulture),
                 UrlEncode(UriBuilder.BaseUrl),
                 UrlEncode(signParameters));

             string key = string.Format(
                 CultureInfo.InvariantCulture,
                 "{0}&{1}",
                 UrlEncode(this.Tokens.ConsumerSecret),
                 UrlEncode(this.Tokens.AccessTokenSecret));

             HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(key));
             byte[] signatureBytes = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(signatureBaseString));
             return Convert.ToBase64String(signatureBytes);
         }


         private string GenerateNonce()
         {
             return new Random()
                 .Next(123400, int.MaxValue)
                 .ToString("X", CultureInfo.InvariantCulture);
         }

         private string GenerateTimeStamp()
         {
             TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
             return Convert.ToInt64(ts.TotalSeconds, CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture);
         }

         private static string UrlEncode(IDictionary<string, string> parameters)
         {
             StringBuilder parameterString = new StringBuilder();

             foreach (var item in parameters)
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

             return UrlEncode(parameterString.ToString());
         }

         /// <summary>
         /// Encodes a value for inclusion in a URL querystring.
         /// </summary>
         /// <param name="value">The value to Url encode</param>
         /// <returns>Returns a Url encoded string</returns>
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
         }  */
    }
}

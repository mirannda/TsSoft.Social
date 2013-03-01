namespace TsSoft.Social.Twitter
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using TsSoft.Social.Api;
    using TsSoft.Social.OAuth;

    /// <author>Evgeniy Yaroslavov</author>
    public class TwitterRequest: HttpRequest
    {
        public AccessCredentials Tokens { get; set; }

        protected IDictionary<string, string> Parameters { get; protected set; }

        public TwitterRequest()
        {
            Tokens = new AccessCredentials();
            Parameters = new Dictionary<string, string>();
        }

        protected virtual void Sign()
        {
            var headerGenerator = new OAuthHeaderGenerator(new Uri(UriBuilder.BaseUrl), Method, Tokens);
            foreach (var parameter in Parameters)
            {
                headerGenerator.Parameters.Add(parameter.Key, parameter.Value);
                UriBuilder.Append(parameter.Key, parameter.Value);
            }
            Headers.Add("Authorization", headerGenerator.Execute());
        }

        protected override string GetResponse()
        {
            return base.GetResponse();
        }


        protected static string ParseParameter(string parameterName, string source)
        {
            Match expressionMatch = Regex.Match(source, string.Format(@"{0}=(?<value>[^&]+)", parameterName));
            return expressionMatch.Success ? expressionMatch.Groups["value"].Value : string.Empty;
        }

    }
}

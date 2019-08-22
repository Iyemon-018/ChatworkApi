namespace ChatworkApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class RequestUriGenerator
    {
        private readonly string _baseUri;

        private readonly string _path;

        private static readonly string ParameterSeparator = "&";

        private RequestUriGenerator(string baseUri, string path)
        {
            if (string.IsNullOrWhiteSpace(baseUri)) throw new ArgumentNullException(nameof(baseUri), $"{nameof(baseUri)} cannot be set to null.");
            _baseUri = baseUri;
            _path    = string.IsNullOrWhiteSpace(path) ? string.Empty : path;
        }

        private string Generate(params (string key, object value)[] parameters)
        {
            var requestUri = $"{_baseUri}{_path}";
            var parameter  = string.Join(ParameterSeparator, ConvertToParameter(parameters));

            if (parameters != null
                && parameter.Any()) requestUri += $"?{parameter}";

            return requestUri;
        }

        private IEnumerable<string> ConvertToParameter(IEnumerable<(string key, object value)> parameters)
            => parameters != null && parameters.Any()
                ? parameters.Where(x => x.value != null).Select(x => $"{x.key}={x.value}")
                : Enumerable.Empty<string>();

        public static string Generate(string baseUri, string path, params (string key, object value)[] parameters)
            => new RequestUriGenerator(baseUri, path).Generate(parameters);

    }
}
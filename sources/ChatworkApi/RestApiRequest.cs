namespace ChatworkApi;

using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Cysharp.Web;

internal sealed class RestApiRequest
{
    private readonly string _baseUri;

    private readonly Dictionary<string, object> _queries;

    private RestApiRequest(string uri)
    {
        _baseUri = $"https://api.chatwork.com/v2{uri}";
    }

    public RestApiRequest(string uri, HttpContent content) : this(uri)
    {
        Content = content;
    }

    public RestApiRequest(string uri, Dictionary<string, object> queries) : this(uri)
    {
        _queries = queries;
    }

    public HttpContent Content { get; }

    public string Uri() => _queries is null ? _baseUri : $"{_baseUri}?{WebSerializer.ToQueryString(_queries)}";

    public static RestApiRequest AsFormUrlEncodedContent<T>(string uri, T content) => new(uri, WebSerializer.ToHttpContent(content));

    public static RestApiRequest AsStringContent<T>(string uri, T content)
    {
        var json = JsonSerializer.Serialize(content, Constants.JsonSerializerOption);
        return new(uri, new StringContent(json, Encoding.UTF8, "application/json"));
    }

    public static RestApiRequest AsNonContent(string uri) => new(uri, content: null);

    public static RestApiRequest AsQuery(string uri, Dictionary<string, object> queries) => new(uri, queries);

    public static RestApiRequest AsQuery(string uri) => new(uri, new Dictionary<string, object>());

    public void TryAddQuery<T>(string name, T value)
    {
        // cf. https://stackoverflow.com/a/864860
        if (!EqualityComparer<T>.Default.Equals(value, default))
        {
            _queries[name] = value;
        }
    }

    public void TryAddQuery<T>(string name, T conditionValue, object value)
    {
        // cf. https://stackoverflow.com/a/864860
        if (!EqualityComparer<T>.Default.Equals(conditionValue, default))
        {
            _queries[name] = value;
        }
    }
}
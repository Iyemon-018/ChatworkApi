namespace ChatworkApi;

using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

internal sealed class RestApiResponse
{
    private readonly HttpResponseMessage _response;

    public RestApiResponse(HttpResponseMessage response)
    {
        _response      = response;
        StatusCode     = _response.StatusCode;
        RequestMessage = response.RequestMessage!;
    }

    public HttpStatusCode StatusCode { get; }

    public HttpRequestMessage RequestMessage { get; }

    public async Task<T> DeserializeAsync<T>(JsonSerializerOptions options, CancellationToken cancellationToken = default)
    {
        await using var stream = await _response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

        return (await JsonSerializer.DeserializeAsync<T>(stream, options, cancellationToken).ConfigureAwait(false))!;
    }

    public RateLimit RateLimit() => ChatworkApi.RateLimit.Build(_response);
}
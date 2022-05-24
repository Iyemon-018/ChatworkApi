namespace ChatworkApi;

using System;
using System.Linq;
using System.Net.Http;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// cf. https://developer.chatwork.com/docs/endpoints#api%E3%81%AE%E5%88%A9%E7%94%A8%E5%9B%9E%E6%95%B0%E5%88%B6%E9%99%90%E3%81%AB%E3%81%A4%E3%81%84%E3%81%A6
/// </remarks>
public sealed class RateLimit
{
    public RateLimit(int limit, int remaining, DateTime reset)
    {
        Limit     = limit;
        Remaining = remaining;
        Reset     = reset;
    }

    /// <summary>
    /// API の最大コール回数を取得します。
    /// </summary>
    public int Limit { get; }

    /// <summary>
    /// 残りコール回数を取得します。
    /// </summary>
    public int Remaining { get; }

    /// <summary>
    /// 次に制限がリセットされる時間を取得します。
    /// </summary>
    public DateTime Reset { get; }

    public override string ToString() => $"X-RateLimit-Limit: {Limit}, X-RateLimit-Remaining: {Remaining}, X-RateLimit-Reset: {Reset}";

    internal static RateLimit Build(HttpResponseMessage message)
    {
        var limit     = message.Headers.TryGetValues("X-RateLimit-Limit", out var limitValue) ? int.Parse(limitValue.First()) : 0;
        var remaining = message.Headers.TryGetValues("X-RateLimit-Remaining", out var remainingValue) ? int.Parse(remainingValue.First()) : 0;
        var reset     = (message.Headers.TryGetValues("X-RateLimit-Reset", out var resetValue) ? long.Parse(resetValue.First()) : 0).FromUnixTime();
        return new RateLimit(limit, remaining, reset);
    }
}
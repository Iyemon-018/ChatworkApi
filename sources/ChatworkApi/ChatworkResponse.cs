namespace ChatworkApi;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Models;

public sealed class ChatworkResponse<T>
{
    private readonly Error _error;

    private readonly HttpRequestMessage _requestMessage;

    private ChatworkResponse(RateLimit rateLimit, HttpStatusCode statusCode, HttpRequestMessage requestMessage, bool isSuccess)
    {
        _requestMessage = requestMessage;
        RateLimit       = rateLimit;
        StatusCode      = statusCode;
        RequestUri      = requestMessage.RequestUri;
        IsSuccess       = isSuccess;
    }

    internal ChatworkResponse(T content, RateLimit rateLimit, HttpStatusCode statusCode, HttpRequestMessage requestMessage)
            : this(rateLimit, statusCode, requestMessage, true)
    {
        _requestMessage = requestMessage;
        Content         = content;
    }

    internal ChatworkResponse(RateLimit rateLimit, HttpStatusCode statusCode, HttpRequestMessage requestMessage, Error error)
            : this(rateLimit, statusCode, requestMessage, false)
    {
        _error          = error;
        _requestMessage = requestMessage;
    }

    public bool IsSuccess { get; }

    public Uri RequestUri { get; }

    public T Content { get; }

    public HttpStatusCode StatusCode { get; }

    public RateLimit RateLimit { get; }

    public IEnumerable<string> Errors() => _error.errors;
}
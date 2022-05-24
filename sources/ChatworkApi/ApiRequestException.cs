namespace ChatworkApi;

using System;
using System.Net;
using System.Runtime.Serialization;

public sealed class ApiRequestException : Exception
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Exception"></see> class.</summary>
    public ApiRequestException(HttpStatusCode statusCode, string reasonPhrase)
    {
        StatusCode   = statusCode;
        ReasonPhrase = reasonPhrase;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Exception"></see> class with serialized data.</summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="info">info</paramref> parameter is null.</exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"></see> is zero (0).</exception>
    public ApiRequestException(HttpStatusCode statusCode, string reasonPhrase, SerializationInfo info, StreamingContext context) :
            base(info, context)
    {
        StatusCode   = statusCode;
        ReasonPhrase = reasonPhrase;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message.</summary>
    /// <param name="message">The message that describes the error.</param>
    public ApiRequestException(HttpStatusCode statusCode, string reasonPhrase, string message) : base(message)
    {
        StatusCode   = statusCode;
        ReasonPhrase = reasonPhrase;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Exception"></see> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
    public ApiRequestException(HttpStatusCode statusCode, string reasonPhrase, string message, Exception innerException) : base(
            message, innerException)
    {
        StatusCode   = statusCode;
        ReasonPhrase = reasonPhrase;
    }

    public HttpStatusCode StatusCode { get; }

    public string ReasonPhrase { get; }
}
namespace ChatworkApi;

using Api;

/// <summary>
/// Chatwork API を呼び出すためのクライアント インターフェースです。
/// Chatwork へのアクセスにはこのインターフェースを使用してください。
/// </summary>
public interface IClient
{
    IMeApi Me { get; }

    IMyApi My { get; }

    IContactsApi Contacts { get; }

    IRoomsApi Rooms { get; }

    IIncomingRequestsApi IncomingRequests { get; }

    void ApiToken(string apiToken);
}
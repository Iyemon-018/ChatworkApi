namespace ChatworkApi.Messages;

/// <summary>
/// ユーザーから受け取ったメッセージの返信を作成するための機能を提供するインターフェースです。
/// </summary>
public interface IReplyMessage
{
    /// <summary>
    /// 指定したユーザーへの返信メッセージを本文の末尾に追記します。
    /// </summary>
    /// <param name="accountId">返信先のアカウントID</param>
    /// <param name="roomId">返信対象のメッセージがあるルームID</param>
    /// <param name="messageId">返信対象のメッセージID</param>
    /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
    IMessageBuilder Add(int    accountId
                      , int    roomId
                      , string messageId);
}
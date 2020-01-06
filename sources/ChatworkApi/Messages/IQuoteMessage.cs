namespace ChatworkApi.Messages
{
    using System;

    /// <summary>
    /// 引用メッセージを構築するためのインターフェースです。
    /// </summary>
    public interface IQuoteMessage
    {
        /// <summary>
        /// 指定したメッセージの引用を構築します。
        /// </summary>
        /// <param name="accountId">ユーザーアカウントID</param>
        /// <param name="time">引用したメッセージの日時</param>
        /// <param name="body">引用対象のメッセージ本文</param>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder Add(int      accountId
                          , DateTime time
                          , string   body);
    }
}
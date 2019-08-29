namespace ChatworkApi.Messages
{
    /// <summary>
    /// インフォメーションの通知を作成するための機能を提供するインターフェースです。
    /// </summary>
    public interface IInformationMessage
    {
        /// <summary>
        /// 指定したメッセージをインフォメーション通知として本文の末尾に追記します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder Add(string message);

        /// <summary>
        /// 指定したメッセージをインフォメーション通知として本文の末尾に追記します。
        /// </summary>
        /// <param name="title">インフォメーションのタイトル</param>
        /// <param name="message">メッセージ</param>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder Add(string title
                          , string message);
    }
}
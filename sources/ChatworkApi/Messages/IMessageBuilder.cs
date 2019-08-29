namespace ChatworkApi.Messages
{
    /// <summary>
    /// Chatwork 用のメッセージを構築するための機能を提供するインターフェースです。
    /// </summary>
    public interface IMessageBuilder
    {
        /// <summary>
        /// 対象のアカウントへの通知を作成する機能を取得します。
        /// </summary>
        IToMessage To { get; }

        /// <summary>
        /// 対象のメッセージへの返信を作成するための機能を取得します。
        /// </summary>
        IReplyMessage Reply { get; }

        /// <summary>
        /// インフォメーションを作成するための機能を取得します。
        /// </summary>
        IInformationMessage Information { get; }

        /// <summary>
        /// メッセージを構築します。
        /// </summary>
        /// <returns>構築したメッセージを返します。</returns>
        string Build();

        /// <summary>
        /// メッセージ本文を現在のメッセージへ追記します。
        /// </summary>
        /// <param name="message">追記するメッセージ</param>
        /// <returns>メッセージを作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder Add(string message);

        /// <summary>
        /// 現在のメッセージ本文末尾に改行を追加します。
        /// </summary>
        /// <returns>開業を追加した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder AddNewLine();

        /// <summary>
        /// 現在のメッセージ本文末尾に罫線を追加します。
        /// </summary>
        /// <returns>罫線を追加した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder AddRuledLine();
    }
}
namespace ChatworkApi.Messages
{
    using System.Text;
    using Models;

    /// <summary>
    /// Chatwork 用のメッセージを構築するための機能を提供するクラスです。
    /// </summary>
    /// <remarks>http://developer.chatwork.com/ja/messagenotation.html</remarks>
    public sealed partial class MessageBuilder : IMessageBuilder, IToMessage, IReplyMessage, IInformationMessage
    {
        /// <summary>
        /// メッセージを構築するためのインスタンスです。
        /// </summary>
        private readonly StringBuilder _message = new StringBuilder();

        /// <summary>
        /// メッセージ本文を現在のメッセージへ追記します。
        /// </summary>
        /// <param name="message">追記するメッセージ</param>
        /// <returns>メッセージを作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        public IMessageBuilder Add(string message)
        {
            _message.Append(message);

            return this;
        }

        /// <summary>
        /// 現在のメッセージ本文末尾に改行を追加します。
        /// </summary>
        /// <returns>開業を追加した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        public IMessageBuilder AddNewLine()
        {
            _message.AppendLine();

            return this;
        }

        /// <summary>
        /// 現在のメッセージ本文末尾に罫線を追加します。
        /// </summary>
        /// <returns>罫線を追加した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        public IMessageBuilder AddRuledLine() => Add("[hr]");

        /// <summary>
        /// メッセージを構築します。
        /// </summary>
        /// <returns>構築したメッセージを返します。</returns>
        public string Build()
        {
            var result = _message.ToString();
            _message.Clear();
            return result;
        }
    }

    public partial class MessageBuilder
    {
        /// <summary>
        /// 対象のアカウントへの通知を作成する機能を取得します。
        /// </summary>
        public IToMessage To => this;

        /// <summary>
        /// 指定したアカウントへの通知をメッセージへ追加します。
        /// </summary>
        /// <param name="accountId">通知先のアカウントID</param>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder IToMessage.Add(int accountId) => Add($"[To:{accountId}]");


        /// <summary>
        /// 指定したアカウントへの通知をメッセージへ追加します。
        /// </summary>
        /// <param name="accountId">通知先のアカウントID</param>
        /// <param name="name">ユーザー名</param>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder IToMessage.Add(int    accountId
                                     , string name) => Add($"[To:{accountId}]{name}");

        /// <summary>
        /// 指定したアカウントへの通知をメッセージへ追加します。
        /// </summary>
        /// <param name="account">通知先のアカウント</param>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder IToMessage.Add(Account account) => To.Add(account.account_id, account.name);

        /// <summary>
        /// 現在のルーム内の全メンバーへの通知をメッセージへ追加します。
        /// </summary>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder IToMessage.All() => Add("[toall]");
    }

    public partial class MessageBuilder
    {
        /// <summary>
        /// 対象のメッセージへの返信を作成するための機能を取得します。
        /// </summary>
        public IReplyMessage Reply => this;

        /// <summary>
        /// 指定したユーザーへの返信メッセージを本文の末尾に追記します。
        /// </summary>
        /// <param name="accountId">返信先のアカウントID</param>
        /// <param name="roomId">返信対象のメッセージがあるルームID</param>
        /// <param name="messageId">返信対象のメッセージID</param>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder IReplyMessage.Add(int accountId
                                        , int roomId
                                        , int messageId)
            => Add($"[rp aid={accountId} to={roomId}-{messageId}]");
    }

    public partial class MessageBuilder
    {
        /// <summary>
        /// インフォメーションを作成するための機能を取得します。
        /// </summary>
        public IInformationMessage Information => this;

        /// <summary>
        /// 指定したメッセージをインフォメーション通知として本文の末尾に追記します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder IInformationMessage.Add(string message) => Add($"[info]{message}[/info]");

        /// <summary>
        /// 指定したメッセージをインフォメーション通知として本文の末尾に追記します。
        /// </summary>
        /// <param name="title">インフォメーションのタイトル</param>
        /// <param name="message">メッセージ</param>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder IInformationMessage.Add(string title
                                              , string message)
            => Add($"[info][title]{title}[/title]{message}[/info]");
    }
}
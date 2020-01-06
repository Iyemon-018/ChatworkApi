namespace ChatworkApi.Messages
{
    using Models;

    /// <summary>
    /// 対象のアカウントへの通知を作成するインターフェースです。
    /// </summary>
    public interface IToMessage
    {
        /// <summary>
        /// 指定したアカウントへの通知をメッセージへ追加します。
        /// </summary>
        /// <param name="accountId">通知先のアカウントID</param>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder Add(int accountId);

        /// <summary>
        /// 指定したアカウントへの通知をメッセージへ追加します。
        /// </summary>
        /// <param name="accountId">通知先のアカウントID</param>
        /// <param name="name">ユーザー名</param>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder Add(int    accountId
                          , string name);

        /// <summary>
        /// 指定したアカウントへの通知をメッセージへ追加します。
        /// </summary>
        /// <param name="account">通知先のアカウント</param>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder Add(Account account);

        /// <summary>
        /// 現在のルーム内の全メンバーへの通知をメッセージへ追加します。
        /// </summary>
        /// <returns>通知を作成した <see cref="IMessageBuilder"/> オブジェクトを返します。</returns>
        IMessageBuilder All();
    }
}
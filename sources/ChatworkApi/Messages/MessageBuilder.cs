namespace ChatworkApi.Messages
{
    using System.Text;

    public sealed partial class MessageBuilder : IMessageBuilder, IToMessage, IReplyMessage, IInformationMessage
    {
        private readonly StringBuilder _message = new StringBuilder();

        public IMessageBuilder Add(string message)
        {
            _message.Append(message);

            return this;
        }

        public IMessageBuilder AddNewLine()
        {
            _message.AppendLine();

            return this;
        }

        public string Build() => _message.ToString();
    }

    public interface IToMessage
    {
        IMessageBuilder Add(int accountId);
    }

    public partial class MessageBuilder
    {
        public IToMessage To => this;

        IMessageBuilder IToMessage.Add(int accountId)
        {
            _message.Append($"[To:{accountId}]");

            return this;
        }
    }

    public interface IReplyMessage
    {
        IMessageBuilder Add(int accountId
                          , int roomId
                          , int messageId);
    }

    public partial class MessageBuilder
    {
        public IReplyMessage Reply => this;

        IMessageBuilder IReplyMessage.Add(int accountId
                                        , int roomId
                                        , int messageId)
        {
            _message.Append($"[rp aid={accountId} to={roomId}-{messageId}]");

            return this;
        }
    }

    public interface IMessageBuilder
    {
        IToMessage To { get; }

        IReplyMessage Reply { get; }

        IInformationMessage Information { get; }

        string Build();

        IMessageBuilder Add(string message);

        IMessageBuilder AddNewLine();
    }

    public interface IInformationMessage
    {
        IMessageBuilder Add(string message);

        IMessageBuilder Add(string title
                          , string message);
    }

    public partial class MessageBuilder
    {
        public IInformationMessage Information => this;

        IMessageBuilder IInformationMessage.Add(string message)
        {
            _message.Append($"[info]{message}[/info]");

            return this;
        }

        IMessageBuilder IInformationMessage.Add(string title
                                              , string message)
        {
            _message.Append($"[info][title]{title}[/title]{message}[/info]");

            return this;
        }
    }
}
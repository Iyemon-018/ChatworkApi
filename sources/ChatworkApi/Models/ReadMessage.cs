namespace ChatworkApi.Models;

public sealed class ReadMessage
{
    public int unread_num { get; set; }

    public int mention_num { get; set; }
}
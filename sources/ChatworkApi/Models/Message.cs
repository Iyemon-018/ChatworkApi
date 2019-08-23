namespace ChatworkApi.Models
{
    public sealed class Message
    {
        public string message_id { get; set; }

        public Account account { get; set; }

        public string body { get; set; }

        public int send_time { get; set; }

        public int update_time { get; set; }
    }
}
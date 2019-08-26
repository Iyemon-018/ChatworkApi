namespace ChatworkApi.Models
{
    using System;
    using Newtonsoft.Json;

    public sealed class Message
    {
        public string message_id { get; set; }

        public Account account { get; set; }

        public string body { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? send_time { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? update_time { get; set; }
    }
}
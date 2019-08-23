namespace ChatworkApi.Models
{
    using System;
    using Newtonsoft.Json;

    public sealed class MyTask
    {
        public int task_id { get; set; }

        public Room room { get; set; }

        public Account assigned_by_account { get; set; }

        public string message_id { get; set; }

        public string body { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? limit_time { get; set; }

        public string status { get; set; }

        public string limit_type { get; set; }
    }
}
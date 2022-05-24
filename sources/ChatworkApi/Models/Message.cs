namespace ChatworkApi.Models;

using System;
using System.Text.Json.Serialization;

public sealed class Message
{
    public string message_id { get; set; }

    public Account account { get; set; }

    public string body { get; set; }
        
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? send_time { get; set; }
        
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? update_time { get; set; }
}
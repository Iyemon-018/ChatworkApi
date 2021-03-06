namespace ChatworkApi.Models;

using System;
using System.Text.Json.Serialization;

public class MyRoom
{
    public int room_id { get; set; }

    public string name { get; set; }

    public string type { get; set; }

    public string role { get; set; }

    public bool sticky { get; set; }

    public int unread_num { get; set; }

    public int mention_num { get; set; }

    public int mytask_num { get; set; }

    public int message_num { get; set; }

    public int file_num { get; set; }

    public int task_num { get; set; }

    public string icon_path { get; set; }
        
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? last_update_time { get; set; }
}
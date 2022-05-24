namespace ChatworkApi.Models;

public sealed class FileData
{
    public int file_id { get; set; }

    public Account account { get; set; }

    public string message_id { get; set; }

    public string filename { get; set; }

    public int filesize { get; set; }

    public int upload_time { get; set; }
}
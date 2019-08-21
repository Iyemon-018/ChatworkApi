namespace ChatworkApi.Models
{
    public sealed class MyTaskModel
    {
        public TaskModel[] Tasks { get; set; }
    }

    public sealed class TaskModel
    {
        public int task_id { get; set; }

        public Room room { get; set; }

        public Account assigned_by_account { get; set; }

        public string message_id { get; set; }

        public string body { get; set; }

        public int limit_time { get; set; }

        public string status { get; set; }

        public string limit_type { get; set; }
    }

    public sealed class Room
    {
        public int room_id { get; set; }

        public string name { get; set; }

        public string icon_path { get; set; }
    }

    public sealed class Account
    {
        public int account_id { get; set; }

        public string name { get; set; }

        public string avatar_image_url { get; set; }
    }
}
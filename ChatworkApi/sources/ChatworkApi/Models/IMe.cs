namespace ChatworkApi.Models
{
    public interface IMe
    {
        int account_id { get; set; }

        int room_id { get; set; }

        string name { get; set; }

        string chatwork_id { get; set; }

        int organization_id { get; set; }

        string organization_name { get; set; }

        string department { get; set; }

        string title { get; set; }

        string url { get; set; }

        string introduction { get; set; }

        string mail { get; set; }

        string tel_organization { get; set; }

        string tel_extension { get; set; }

        string tel_mobile { get; set; }

        string skype { get; set; }

        string facebook { get; set; }

        string twitter { get; set; }

        string avatar_image_url { get; set; }

        string login_mail { get; set; }
    }

    internal sealed class Me : IMe
    {
        public int account_id { get; set; }
        public int room_id { get; set; }
        public string name { get; set; }
        public string chatwork_id { get; set; }
        public int organization_id { get; set; }
        public string organization_name { get; set; }
        public string department { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string introduction { get; set; }
        public string mail { get; set; }
        public string tel_organization { get; set; }
        public string tel_extension { get; set; }
        public string tel_mobile { get; set; }
        public string skype { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string avatar_image_url { get; set; }
        public string login_mail { get; set; }
    }
}
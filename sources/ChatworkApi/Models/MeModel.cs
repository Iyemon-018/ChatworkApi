namespace ChatworkApi.Models
{
    public sealed class MeModel
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
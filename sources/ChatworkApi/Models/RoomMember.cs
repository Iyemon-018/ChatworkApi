namespace ChatworkApi.Models;

public class RoomMember
{
    public int account_id { get; set; }

    public string role { get; set; }

    public string name { get; set; }

    public string chatwork_id { get; set; }

    public int organization_id { get; set; }

    public string organization_name { get; set; }

    public string department { get; set; }

    public string avatar_image_url { get; set; }
}
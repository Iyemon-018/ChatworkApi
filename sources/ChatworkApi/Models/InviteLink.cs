namespace ChatworkApi.Models;

public sealed class InviteLink
{
    public bool _public { get; set; }

    public string url { get; set; }

    public bool need_acceptance { get; set; }

    public string description { get; set; }
}
namespace ChatworkApi.Models
{
    public sealed class UpdatedRoomMember
    {
        public int[] admin { get; set; }

        public int[] member { get; set; }

        public int[] _readonly { get; set; }
    }
}
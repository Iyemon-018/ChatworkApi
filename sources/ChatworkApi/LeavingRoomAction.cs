namespace ChatworkApi
{
    /// <summary>
    /// グループチャットを退室する際のアクション種別を定義します。
    /// </summary>
    public enum LeavingRoomAction
    {
        [Alias("leave")]
        Leave,

        [Alias("delete")]
        Delete,
    }
}
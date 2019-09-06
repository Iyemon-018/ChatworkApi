namespace ChatworkApi
{
    /// <summary>
    /// タスクの期限種別を定義します。
    /// </summary>
    public enum TaskLimitType
    {
        [Alias("none")]
        None,

        [Alias("date")]
        Date,

        [Alias("time")]
        Time,
    }
}
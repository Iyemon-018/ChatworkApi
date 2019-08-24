namespace ChatworkApi
{
    /// <summary>
    /// タスクの期限種別を定義します。
    /// </summary>
    public enum TaskLimitType
    {
        [ParameterValue("none")]
        None,

        [ParameterValue("date")]
        Date,

        [ParameterValue("time")]
        Time,
    }
}
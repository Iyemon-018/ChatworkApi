namespace ChatworkApi
{
    /// <summary>
    /// タスクの状態を定義します。
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        /// オープンであることを表します。
        /// </summary>
        [ParameterValue("open")]
        Open,

        /// <summary>
        /// 完了していることを表します。
        /// </summary>
        [ParameterValue("done")]
        Done,
    }
}
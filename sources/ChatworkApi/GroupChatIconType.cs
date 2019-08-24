namespace ChatworkApi
{
    /// <summary>
    /// グループチャットに表示するアイコン種別を定義します。
    /// </summary>
    public enum GroupChatIconType
    {
        [ParameterValue("group")]
        Group,

        [ParameterValue("check")]
        Check,

        [ParameterValue("document")]
        Document,

        [ParameterValue("meeting")]
        Meeting,

        [ParameterValue("event")]
        Event,

        [ParameterValue("project")]
        Project,

        [ParameterValue("business")]
        Business,

        [ParameterValue("study")]
        Study,

        [ParameterValue("security")]
        Security,

        [ParameterValue("star")]
        Star,

        [ParameterValue("idea")]
        Idea,

        [ParameterValue("heart")]
        Heart,

        [ParameterValue("magcup")]
        Magcup,

        [ParameterValue("beer")]
        Beer,

        [ParameterValue("music")]
        Music,

        [ParameterValue("sports")]
        Sports,

        [ParameterValue("travel")]
        Travel
    }
}
namespace ChatworkApi;

internal static class QueryParameterConvertExtensions
{
    public static int AsParameter(this bool self) => self ? 1 : 0;
}
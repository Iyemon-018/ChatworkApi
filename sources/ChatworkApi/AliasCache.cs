namespace ChatworkApi;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// <see cref="AliasAttribute"/> を定義した列挙子をキャッシュするための仕組みを提供します。
/// </summary>
/// <typeparam name="TEnum"><see cref="AliasAttribute"/> を定義した列挙型</typeparam>
internal static class AliasCache<TEnum>
{
    private static Dictionary<TEnum, string> Values { get; }

    static AliasCache()
    {
        var enumType = typeof(TEnum);
        if (!enumType.IsEnum) throw new TypeAccessException($"{enumType.FullName} is not enum type.");

        var fields = Enum.GetValues(enumType).OfType<TEnum>().ToArray();
        Values = fields.ToDictionary(x => x
              , x => enumType.GetField(x.ToString())
                             .GetCustomAttribute<AliasAttribute>()
                            ?.Value);
    }

    /// <summary>
    /// 指定した列挙子に定義した<see cref="AliasAttribute"/> の値を取得します。
    /// </summary>
    /// <param name="enumValue">列挙子の値</param>
    /// <returns><see cref="AliasAttribute"/> の値を返します。</returns>
    public static string Value(TEnum enumValue) => Values[enumValue];
}
namespace ChatworkApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class ParameterGenerator
    {
        /// <summary>
        /// <c>key</c> と <c>value</c> で定義された Tuple 配列のパラメータを <see cref="KeyValuePair{TKey,TValue}"/> のシーケンスへ変換します。
        /// </summary>
        /// <param name="parameters">変換元のパラメータ</param>
        /// <returns>変換した結果を返します。</returns>
        public static IEnumerable<KeyValuePair<string, string>> ConvertToParameters(params (string key, object value)[] parameters)
            => parameters.Where(x => x.value != null)
                         .Select(x => new KeyValuePair<string, string>(x.key, ConvertToValue(x.value)));

        /// <summary>
        /// <see cref="object"/> 型の値を API 呼び出し用の適切な文字列へ変換します。
        /// </summary>
        /// <param name="value">変換元の値</param>
        /// <returns>変換結果文字列を返します。</returns>
        public static string ConvertToValue(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value is IEnumerable<int> intArray) return string.Join(",", intArray);

            if (value is DateTime dateTime) return dateTime.ToUnixTime().ToString();

            var flag = value as bool?;
            if (flag.HasValue) return flag.Value ? "1" : "0";

            return value.ToString();
        }
    }
}
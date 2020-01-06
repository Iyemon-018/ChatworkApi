namespace ChatworkApi
{
    using System;

    /// <summary>
    /// <see cref="Enum"/> 型に関する拡張メソッドを定義します。
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 列挙体の値に定義されている<see cref="AliasAttribute"/> の値へ変換します。
        /// </summary>
        /// <typeparam name="TEnum">列挙体の型</typeparam>
        /// <param name="self">自分自身</param>
        /// <returns>変換結果を返します。</returns>
        /// <exception cref="TypeAccessException">型が <see cref="Enum"/> ではない場合にスローされます。</exception>
        /// <example>
        /// 次のような列挙体が定義されている場合のコード例を示します。
        /// <code><![CDATA[
        /// public enum TestType
        /// {
        ///     [Alias("method")]
        ///     Method,
        ///
        ///     [Alias("property")]
        ///     Property,
        ///
        ///     [Alias("inner class")]
        ///     InnerClass,
        ///
        ///     None,
        /// }
        /// ]]></code>
        /// 
        /// 列挙子に定義した文字列を取得する例を以下に示します。
        ///
        /// <code><![CDATA[
        /// TestType testType = TestType.Method;
        ///
        /// // parameterValue = "method";
        /// string parameterValue = testType.ToAlias();
        /// ]]></code>
        ///
        /// 列挙子が <see cref="Nullable"/> の場合は次のようなコードになります。
        /// <code><![CDATA[
        /// // testType is not null.
        /// TestType? testType = GetTestType();
        ///
        /// // parameterValue = "property";
        /// string parameterValue = testType?.ToAlias();
        /// ]]></code>
        /// <code><![CDATA[
        /// // testType is null.
        /// TestType? testType = GetNullTestType();
        ///
        /// // parameterValue = null;
        /// string parameterValue = testType?.ToAlias(); 
        /// ]]></code>
        /// </example>
        public static string ToAlias<TEnum>(this TEnum self) where TEnum : struct
        {
            var enumType = typeof(TEnum);

            if (!enumType.IsEnum) throw new TypeAccessException($"{enumType.FullName} is not enum type.");

            return AliasCache<TEnum>.Value(self);
        }
    }
}
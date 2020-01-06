namespace ChatworkApi.Tests
{
    using System.Linq;

    /// <summary>
    /// テストで使用する値に関する拡張メソッドを定義します。
    /// </summary>
    public static class TestValueExtensions
    {
        /// <summary>
        /// 結果文字列を出力用に変換します。
        /// </summary>
        /// <param name="self">出力文字列</param>
        /// <param name="nullValue"><c>null</c> の場合に出力数文字列</param>
        /// <param name="emptyValue">空文字の場合に出力する文字列</param>
        /// <param name="whiteSpaceValue">すべてホワイトスペースの場合に出力する文字列</param>
        /// <returns>変換結果を返します。</returns>
        public static string ToValue(this string self
                                   , string      nullValue       = "null"
                                   , string      emptyValue      = "empty"
                                   , string      whiteSpaceValue = "whitespace")
        {
            if (self == null) return nullValue;
            if (string.IsNullOrEmpty(self)) return emptyValue;
            if (self.All(x => x == ' ')) return whiteSpaceValue;
            return self;
        }
    }
}
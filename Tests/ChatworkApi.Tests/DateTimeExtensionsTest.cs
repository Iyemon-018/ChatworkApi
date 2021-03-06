namespace ChatworkApi.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Xunit.Abstractions;

    public class DateTimeExtensionsTest : TestBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="outputHelper">テスト結果を出力するためのプロバイダー</param>
        public DateTimeExtensionsTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        private static readonly TimeSpan UtcOffset = TimeZoneInfo.Local.BaseUtcOffset;

        private static readonly int UtcOffsetTotalSeconds = (int)UtcOffset.TotalSeconds;

        #region ToUnixTime Method

        public static IEnumerable<object[]> Get_Test_正常_ToUnixTime_Data()
        {
            yield return new object[]
                         {
                             new DateTime(1970, 1, 1, 0,0,0)
                           , UtcOffsetTotalSeconds * -1
                         };

            yield return new object[]
                         {
                             new DateTime(1970, 1, 1, 0,0,1)
                           , (UtcOffsetTotalSeconds * -1) + 1
                         };

            yield return new object[]
                         {
                             new DateTime(2019, 8, 24, 2,57,46)
                           , ((new DateTime(2019, 8, 24, 2,57,46) - new DateTime(1970, 1, 1)) - UtcOffset).TotalSeconds
                         };

            yield return new object[]
                         {
                             new DateTime(2038, 1, 19, 3,14,7)
                           , ((new DateTime(2038, 1, 19, 3,14,7) - new DateTime(1970, 1, 1)) - UtcOffset).TotalSeconds
                         };
        }

        [Theory]
        [MemberData(nameof(Get_Test_正常_ToUnixTime_Data))]
        public void Test_正常_ToUnixTime(DateTime target
                                     , long     expected)
        {
            // arrange

            // act
            var actual = target.ToUnixTime();

            // assert
            Assert.Equal(expected, actual);
            Output($"日時:{target:yyyy/MM/dd HH:mm:ss} → UNIX時刻:{actual}");
        }

        #endregion

        #region FromUnixTime Method

        public static IEnumerable<object[]> Get_Test_正常_FromUnixTime_Data()
        {
            // ここでUNIX日時を調べた。
            // https://keisan.casio.jp/exec/system/1526004418
            yield return new object[]
                         {
                             0
                             , new DateTime(1970, 1, 1,0, 0, 0).Add(UtcOffset), 
                         };

            yield return new object[]
                         {
                             1
                           , new DateTime(1970, 1, 1,0, 0, 0).Add(UtcOffset).AddSeconds(1),
                         };

            yield return new object[]
                         {
                             1566613821
                           , new DateTime(1970, 1, 1,0, 0, 0).Add(UtcOffset).AddSeconds(1566613821),
                         };
        }

        [Theory]
        [MemberData(nameof(Get_Test_正常_FromUnixTime_Data))]
        public void Test_正常_FromUnixTime(long     target
                                       , DateTime expected)
        {
            // arrange

            // act
            var actual = target.FromUnixTime();

            // assert
            Assert.Equal(expected, actual);
            Output($"UNIX時刻:{target} → 日時:{actual:yyyy/MM/dd HH:mm:ss}");
        }

        #endregion
    }
}
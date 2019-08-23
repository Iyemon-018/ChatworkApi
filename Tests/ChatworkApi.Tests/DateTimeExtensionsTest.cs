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

        #region ToUnixTime Method

        public static IEnumerable<object[]> Get_Test_正常_ToUnixTime_Data()
        {
            yield return new object[]
                         {
                             new DateTime(1970, 1, 1, 0,0,0)
                           , 0
                         };

            yield return new object[]
                         {
                             new DateTime(1970, 1, 1, 0,0,1)
                           , 1
                         };

            yield return new object[]
                         {
                             new DateTime(2019, 8, 24, 2,57,46)
                           , 1566615466
                         };

            yield return new object[]
                         {
                             new DateTime(2038, 1, 19, 3,14,7)
                           , int.MaxValue
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
            yield return new object[]
                         {
                             0
                             , new DateTimeOffset(new DateTime(1970, 1, 1,0, 0, 0)).LocalDateTime, 
                         };

            yield return new object[]
                         {
                             1
                           , new DateTimeOffset(new DateTime(1970, 1, 1,0, 0, 1)).LocalDateTime,
                         };

            yield return new object[]
                         {
                             1566613821
                           , new DateTimeOffset(new DateTime(2019, 8, 24,2, 30, 21)).LocalDateTime,
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
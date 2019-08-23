namespace ChatworkApi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using Xunit.Abstractions;

    public class ApiClientTest : TestBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="outputHelper">テスト結果を出力するためのプロバイダー</param>
        public ApiClientTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        #region ConvertToValue Method

        public static IEnumerable<object[]> Get_Test_正常_ConvertToValue_Data()
        {
            yield return new object[]
                         {
                             int.MaxValue
                           , int.MaxValue.ToString()
                         };

            yield return new object[]
                         {
                             double.MinValue
                           , double.MinValue.ToString()
                         };

            yield return new object[]
                         {
                             (int?) int.MinValue
                           , int.MinValue.ToString()
                         };

            yield return new object[]
                         {
                             new int[] {1, 2, 3, 10, 20, 30}
                           , "1,2,3,10,20,30"
                         };

            yield return new object[]
                         {
                             true
                           , "1"
                         };

            yield return new object[]
                         {
                             false
                           , "0"
                         };
        }

        [Theory]
        [MemberData(nameof(Get_Test_正常_ConvertToValue_Data))]
        public void Test_正常_ConvertToValue(object value, string expected)
        {
            // arrange

            // act
            var actual = ApiClient.ConvertToValue(value);

            // assert
            Assert.Equal(expected, actual);
            Output(actual);
        }

        public static IEnumerable<object[]> Get_Test_異常_ConvertToValue_Data()
        {
            yield return new object[]
                         {
                             null
                           , typeof(ArgumentNullException)
                         };
        }

        [Theory]
        [MemberData(nameof(Get_Test_異常_ConvertToValue_Data))]
        public void Test_異常_ConvertToValue(object value, Type expectedType)
        {
            // arrange

            // act
            var ex = Record.Exception(() => ApiClient.ConvertToValue(value));

            // assert
            Assert.IsType(expectedType, ex);
            Output(ex);
        }

        #endregion

        #region ConvertToParameters Method

        public static IEnumerable<object[]> Get_Test_正常_ConvertToParameters_Data()
        {
            yield return new object[]
                         {
                             new (string key, object value)[]
                             {
                                 ("integer", 1)
                             }
                           , new[]
                             {
                                 new KeyValuePair<string, string>("integer", "1")
                             }
                            ,
                         };

            yield return new object[]
                         {
                             new (string key, object value)[]
                             {
                                 ("integer", 1)
                               , ("text", "text")
                               , ("flag", false)
                               , ("flagn", (bool?) true)
                               , ("values", new int[] {1, 2, 3})
                             }
                           , new[]
                             {
                                 new KeyValuePair<string, string>("integer", "1")
                               , new KeyValuePair<string, string>("text", "text")
                               , new KeyValuePair<string, string>("flag", "0")
                               , new KeyValuePair<string, string>("flagn", "1")
                               , new KeyValuePair<string, string>("values", "1,2,3")
                             }
                            ,
                         };

            yield return new object[]
                         {
                             new (string key, object value)[]
                             {
                                 ("integer", 1)
                               , ("text", "text")
                               , ("nullValue", null)
                               , ("flag", false)
                               , ("flagn", (bool?) true)
                               , ("values", new int[] {1, 2, 3})
                             }
                           , new[]
                             {
                                 new KeyValuePair<string, string>("integer", "1")
                               , new KeyValuePair<string, string>("text", "text")
                               , new KeyValuePair<string, string>("flag", "0")
                               , new KeyValuePair<string, string>("flagn", "1")
                               , new KeyValuePair<string, string>("values", "1,2,3")
                             }
                         };
            yield return new object[]
                         {
                             Enumerable.Empty<(string key, object value)>()
                             , Enumerable.Empty<KeyValuePair<string, string>>()
                         };
        }

        [Theory]
        [MemberData(nameof(Get_Test_正常_ConvertToParameters_Data))]
        public void Test_正常_ConvertToParameters((string key, object value)[] parameters, IEnumerable<KeyValuePair<string, string>> expected)
        {
            // arrange

            // act
            var actual = ApiClient.ConvertToParameters(parameters);

            // assert
            Assert.Equal(expected, actual);
            Output(string.Join(Environment.NewLine, actual.Select(x => $"[{x.Key}]{x.Value}")));
        }

        public static IEnumerable<object[]> Get_Test_異常_ConvertToParameters_Data()
        {
            yield return new object[]
                         {
                             null
                             , typeof(ArgumentNullException)
                         };
        }

        [Theory]
        [MemberData(nameof(Get_Test_異常_ConvertToParameters_Data))]
        public void Test_異常_ConvertToParameters((string key, object value)[] parameters, Type expectedType)
        {
            // arrange

            // act
            var ex = Record.Exception(() => ApiClient.ConvertToParameters(parameters));

            // assert
            Assert.IsType(expectedType, ex);
            Output(ex);
        }

        #endregion
    }
}
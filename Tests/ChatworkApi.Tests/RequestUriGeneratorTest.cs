namespace ChatworkApi.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Xunit.Abstractions;

    public class RequestUriGeneratorTest : TestBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="outputHelper">テスト結果を出力するためのプロバイダー</param>
        public RequestUriGeneratorTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        #region Generate Method

        public static IEnumerable<object[]> Get_Test_正常_Generate_Data()
        {
            yield return new object[]
                         {
                             "https://example/hoge/v2"
                           , null
                           , "https://example/hoge/v2"
                         };

            yield return new object[]
                         {
                             "https://example/hoge/v2"
                           , string.Empty
                           , "https://example/hoge/v2"
                         };

            yield return new object[]
                         {
                             "https://example/hoge/v2"
                           , "   "
                           , "https://example/hoge/v2"
                         };

            yield return new object[]
                         {
                             "https://example/hoge/v2"
                           , "/fuga"
                           , "https://example/hoge/v2/fuga"
                         };

            yield return new object[]
                         {
                             "https://example/hoge/v2"
                           , "/fuga/bar"
                           , "https://example/hoge/v2/fuga/bar"
                         };
        }

        [Theory]
        [MemberData(nameof(Get_Test_正常_Generate_Data))]
        public void Test_正常_Generate(string baseUri, string path, string expected)
        {
            // arrange

            // act
            var actual = RequestUriGenerator.Generate(baseUri, path);

            // assert
            Assert.Equal(expected, actual);
            Output(actual);
        }

        public static IEnumerable<object[]> Get_Test_異常_Generate_Data()
        {
            yield return new object[]
                         {
                             null
                           , "/fuga"
                           , typeof(ArgumentNullException)
                         };

            yield return new object[]
                         {
                             string.Empty
                           , "/fuga"
                           , typeof(ArgumentNullException)
                         };

            yield return new object[]
                         {
                             "   "
                           , "/fuga"
                           , typeof(ArgumentNullException)
                         };
        }

        [Theory]
        [MemberData(nameof(Get_Test_異常_Generate_Data))]
        public void Test_異常_Generate(string baseUri, string path, Type expectedType)
        {
            // arrange

            // act
            var ex = Record.Exception(() => RequestUriGenerator.Generate(baseUri, path));

            // assert
            Assert.IsType(expectedType, ex);

            Output(ex);
        }

        public static IEnumerable<object[]> Get_Test_正常_Generate_withParameters_Data()
        {
            yield return new object[]
                         {
                             "https://example/hoge/v2"
                           , "/fuga"
                           , null
                           , "https://example/hoge/v2/fuga"
                         };

            yield return new object[]
                         {
                             "https://example/hoge/v2"
                           , "/fuga"
                             , new (string key, object value)[]
                               {
                                   ("bar", 1)
                               }
                             , "https://example/hoge/v2/fuga?bar=1"
                         };

            yield return new object[]
                         {
                             "https://example/hoge/v2"
                           , "/fuga"
                           , new (string key, object value)[]
                             {
                                 ("bar", 1)
                                 , ("vvv", "test")
                                 , ("bear", "kuma")
                             }
                           , "https://example/hoge/v2/fuga?bar=1&vvv=test&bear=kuma"
                         };
        }

        [Theory]
        [MemberData(nameof(Get_Test_正常_Generate_withParameters_Data))]
        public void Test_正常_Generate_withParameters(string baseUri, string path, (string key, object value)[] parameters, string expected)
        {
            // arrange

            // act
            var actual = RequestUriGenerator.Generate(baseUri, path, parameters);

            // assert
            Assert.Equal(expected, actual);
            Output(actual);
        }

        #endregion
    }
}
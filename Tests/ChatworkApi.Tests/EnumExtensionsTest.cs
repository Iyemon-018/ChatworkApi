namespace ChatworkApi.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Xunit.Abstractions;

    public class EnumExtensionsTest : TestBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="outputHelper">テスト結果を出力するためのプロバイダー</param>
        public EnumExtensionsTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        public enum TestType
        {
            [Alias("method")]
            Method,

            [Alias("property")]
            Property,

            [Alias("inner class")]
            InnerClass,

            None,
        }

        #region ToAlias Method

        public static IEnumerable<object[]> Get_Test_正常_ToParameterValue_Data()
        {
            yield return new object[]
                         {
                             TestType.Method
                           , "method"
                         };

            yield return new object[]
                         {
                             TestType.None
                           , null
                         };
        }

        [Theory]
        [MemberData(nameof(Get_Test_正常_ToParameterValue_Data))]
        public void Test_正常_ToParameterValue(TestType target
                                           , string expected)
        {
            // arrange

            // act
            var actual = target.ToAlias();

            // assert
            Assert.Equal(expected, actual);
            Output($"{target.GetType().Name}.{target} = {actual.ToValue()}");
        }

        [Fact]
        public void Test_異常_ToParameterValue_int()
        {
            // arrange

            // act
            var ex = Record.Exception(() => 1.ToAlias());

            // assert
            Assert.IsType<TypeAccessException>(ex);
            Output(ex);
        }

        [Fact]
        public void Test_異常_ToParameterValue_DateTime()
        {
            // arrange

            // act
            var ex = Record.Exception(() => DateTime.Now.ToAlias());

            // assert
            Assert.IsType<TypeAccessException>(ex);
            Output(ex);
        }

        [Fact]
        public void Test_使用例_ToParameterValue_通常パターン()
        {
            TestType testType = TestType.Method;

            // parameterValue = "method";
            string parameterValue = testType.ToAlias();

            Assert.Equal("method", parameterValue);
        }

        private TestType? GetTestType() => TestType.Property;

        [Fact]
        public void Test_使用例_ToParameterValue_Nullableパターン_NotNull()
        {
            // testType is not null.
            TestType? testType = GetTestType();

            // parameterValue = "property";
            string parameterValue = testType?.ToAlias();

            Assert.Equal("property", parameterValue);
        }

        private TestType? GetNullTestType() => null;

        [Fact]
        public void Test_使用例_ToParameterValue_Nullableパターン_Null()
        {
            // testType is null.
            TestType? testType = GetNullTestType();

            // parameterValue = null;
            string parameterValue = testType?.ToAlias();

            Assert.Null(parameterValue);
        }

        #endregion
    }
}
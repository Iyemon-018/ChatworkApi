namespace ChatworkApi.Tests.Messages
{
    using System;
    using ChatworkApi.Messages;
    using Xunit;
    using Xunit.Abstractions;

    public class MessageBuilderTest : TestBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="outputHelper">テスト結果を出力するためのプロバイダー</param>
        public MessageBuilderTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        #region To

        [Fact]
        public void Test_正常_To()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.To.Add(123456).Build();

            // assert
            Assert.Equal("[To:123456]", actual);
            Output(actual);
        }

        [Fact]
        public void Test_正常_To_Message付き()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.To.Add(9876543)
                                   .AddMessage("講義のご連絡ありがとうございます。")
                                   .Build();

            // assert
            Assert.Equal("[To:9876543]講義のご連絡ありがとうございます。", actual);
            Output(actual);
        }

        [Fact]
        public void Test_正常_To_複数回()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.To.Add(123)
                                .To.Add(456)
                                .AddMessage("test")
                                .Build();

            // assert
            Assert.Equal("[To:123][To:456]test", actual);
            Output(actual);
        }

        [Fact]
        public void Test_正常_To_複数回_呼び出し順序()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.To.Add(123)
                                .AddNewLine()
                                .AddMessage($"こんにちは")
                                .AddNewLine()
                                .To.Add(1111)
                                .AddNewLine()
                                .To.Add(2222)
                                .Build();

            // assert
            Assert.Equal($"[To:123]{Environment.NewLine}"
                         + $"こんにちは{Environment.NewLine}"
                         + $"[To:1111]{Environment.NewLine}"
                         + $"[To:2222]"
                       , actual);
            Output(actual);
        }

        #endregion
    }
}
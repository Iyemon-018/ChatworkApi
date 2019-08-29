namespace ChatworkApi.Tests.Messages
{
    using System;
    using ChatworkApi.Messages;
    using Models;
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

        #region Add
        // TODO test Add() method
        #endregion

        #region AddRuledLine

        [Fact]
        public void Test_正常_AddRuledLine()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.AddRuledLine().Build();

            // assert
            Assert.Equal("[hr]", actual);
            Output(actual);
        }

        [Fact]
        public void Test_正常_AddRuledLine_改行付き()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.Add("罫線引いてみた。")
                                .AddNewLine()
                                .AddRuledLine()
                                .AddNewLine()
                                .Add("こんな感じになる。")
                                .Build();

            // assert
            Assert.Equal($"罫線引いてみた。{Environment.NewLine}"
                         + $"[hr]{Environment.NewLine}"
                         + $"こんな感じになる。"
                         ,actual);
            Output(actual);
        }

        #endregion

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
                                   .Add("講義のご連絡ありがとうございます。")
                                   .Build();

            // assert
            Assert.Equal("[To:9876543]講義のご連絡ありがとうございます。", actual);
            Output(actual);
        }

        [Fact]
        public void Test_正常_To_Account指定()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.To.Add(new Account
                                        {
                                            name             = "山田 太郎"
                                          , account_id       = 99912
                                          , avatar_image_url = string.Empty
                                        })
                                .Build();

            // assert
            Assert.Equal("[To:99912]山田 太郎", actual);
            Output(actual);
        }

        [Fact]
        public void Test_正常_To_Account指定_Message付き()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.To.Add(new Account
                                        {
                                            name             = "山田 太郎"
                                          , account_id       = 99912
                                          , avatar_image_url = string.Empty
                                        })
                                .AddNewLine()
                                .Add("講義のご連絡ありがとうございます。")
                                .Build();

            // assert
            Assert.Equal($"[To:99912]山田 太郎{Environment.NewLine}"
                         + $"講義のご連絡ありがとうございます。", actual);
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
                                .Add("test")
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
                                .Add($"こんにちは")
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

        [Fact]
        public void Test_正常_To_ユーザー名指定()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.To.Add(1987, "山田 太郎さん").Build();

            // assert
            Assert.Equal("[To:1987]山田 太郎さん", actual);
        }

        [Fact]
        public void Test_正常_To_ユーザー名指定とメッセージ()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.To.Add(1987, "山田 太郎さん")
                                .AddNewLine()
                                .Add("お疲れさまです。")
                                .Build();

            // assert
            Assert.Equal($"[To:1987]山田 太郎さん{Environment.NewLine}"
                         + $"お疲れさまです。", actual);
        }

        [Fact]
        public void Test_正常_To_All()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.To.All()
                                .Add("重要なお知らせ。")
                                .Build();

            // assert
            Assert.Equal("[toall]重要なお知らせ。", actual);
            Output(actual);
        }

        #endregion

        #region Information

        [Fact]
        public void Test_正常_Information()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.Information.Add("こんにちは")
                                .Build();

            // assert
            Assert.Equal("[info]こんにちは[/info]", actual);
            Output(actual);
        }

        [Fact]
        public void Test_正常_Information_WithTitle()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.Information.Add("タイトル", "こんにちは")
                                .Build();

            // assert
            Assert.Equal("[info][title]タイトル[/title]こんにちは[/info]", actual);
            Output(actual);
        }

        [Fact]
        public void Test_正常_Information_メッセージ例()
        {
            // arrange
            var builder = new MessageBuilder();

            // act
            var actual = builder.Add($"皆さんお疲れさまです。{Environment.NewLine}"
                                     + $"毎年恒例BBQのお知らせです。{Environment.NewLine}")
                                .Information.Add("2019年度 in Summer BBQ開催のお知らせ"
                                                 , $"開催日時 : 2019年8月1日 午前09:00～{Environment.NewLine}"
                                                   + $"時間 : 4時間程度{Environment.NewLine}"
                                                   + $"参加費 : 1,000円")
                                .Add($"参加確認をしますので、2019年7月10日までにご連絡ください。{Environment.NewLine}"
                                     + $"よろしくお願いいたします。")
                                .Build();

            // assert
            Assert.Equal($"皆さんお疲れさまです。{Environment.NewLine}"
                         + $"毎年恒例BBQのお知らせです。{Environment.NewLine}"
                         + $"[info][title]2019年度 in Summer BBQ開催のお知らせ[/title]開催日時 : 2019年8月1日 午前09:00～{Environment.NewLine}"
                         + $"時間 : 4時間程度{Environment.NewLine}"
                         + $"参加費 : 1,000円[/info]"
                         + $"参加確認をしますので、2019年7月10日までにご連絡ください。{Environment.NewLine}"
                         + $"よろしくお願いいたします。"
                       , actual);
            Output(actual);
        }

        #endregion
    }
}
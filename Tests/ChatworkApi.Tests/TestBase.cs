namespace ChatworkApi.Tests
{
    using System;
    using Xunit.Abstractions;

    public abstract class TestBase
    {
        private readonly ITestOutputHelper _outputHelper;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="outputHelper">テスト結果を出力するためのプロバイダー</param>
        protected TestBase(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        /// <summary>
        /// 実行結果を出力します。
        /// </summary>
        /// <param name="message">出力メッセージ</param>
        protected void Output(string message) => _outputHelper.WriteLine(message);

        /// <summary>
        /// 例外がスローされた際の実行結果を出力します。
        /// </summary>
        /// <param name="exception">例外インスタンス</param>
        protected void Output(Exception exception)
            => _outputHelper.WriteLine($"■Exception{Environment.NewLine}"
                                       + $"{exception.Message}{Environment.NewLine}"
                                       + $"・Stack trace{Environment.NewLine}"
                                       + $"{exception.StackTrace}");

        /// <summary>
        /// 例外がスローされた際の実行結果を出力します。
        /// </summary>
        /// <param name="exception">例外インスタンス</param>
        /// <param name="message">出力メッセージ</param>
        protected void Output(Exception exception, string message)
            => _outputHelper.WriteLine($"{message}{Environment.NewLine}"
                                       + $"■Exception{Environment.NewLine}"
                                       + $"{exception.Message}{Environment.NewLine}"
                                       + $"・Stack trace{Environment.NewLine}"
                                       + $"{exception.StackTrace}");
    }
}
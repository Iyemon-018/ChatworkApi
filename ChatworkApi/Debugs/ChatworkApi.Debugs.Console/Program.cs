using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatworkApi.Debugs.Console
{
    using System.IO;
    using Console = System.Console;

    class Program
    {
        static async Task Main(string[] args)
        {
            var apiToken = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "api-token.txt"));
            var apiClient = new ChatworkApi.ApiClient(apiToken);
            var result = await apiClient.GetAsync($"/me");
            
            Console.WriteLine(result);
        }
    }
}

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
        static void Main(string[] args)
        {
            var apiToken = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "api-token.txt"));
            var client = new ClientApi(apiToken);
            var me = client.Me.GetAsync().Result;
            
            Console.WriteLine($"所属:{me.organization_name}, 氏名:{me.name}, ID:{me.account_id}");
        }
    }
}

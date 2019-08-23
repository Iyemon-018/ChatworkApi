namespace ChatworkApi.Debugs.Console
{
    using System;
    using System.IO;
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var apiToken = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "api-token.txt"));
            var client   = new ClientApi(apiToken);
            var me       = client.GetMeAsync().Result;

            Console.WriteLine($"所属:{me.organization_name}, 氏名:{me.name}, ID:{me.account_id}");

            var myStatus = client.GetMyStatusAsync().Result;

            Console.WriteLine($"未読のある部屋の数:{myStatus.unread_room_num}, 返信のある部屋の数:{myStatus.mention_room_num}, タスクのある部屋の数:{myStatus.mytask_room_num}"
                              + $", 未読の数:{myStatus.unread_num}, 返信の数:{myStatus.mention_num}, タスクの数:{myStatus.mention_num}");

            var contacts = client.GetContactsAsync().Result
                                 .OrderBy(x => x.account_id)
                                 .Select(x => $"{x.organization_name}:{x.name}[{x.department}]")
                                 .ToArray();

            Console.WriteLine("- 自分のコンタクト一覧");
            Console.WriteLine($"{string.Join(Environment.NewLine, contacts)}");
        }
    }
}
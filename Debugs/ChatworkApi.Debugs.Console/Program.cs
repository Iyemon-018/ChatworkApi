namespace ChatworkApi.Debugs.Console
{
    using System;
    using System.IO;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var apiToken = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "api-token.txt"));
            var client   = new ClientApi(apiToken);
            var me       = client.Me.GetAsync().Result;

            Console.WriteLine($"所属:{me.organization_name}, 氏名:{me.name}, ID:{me.account_id}");

            var myStatus = client.My.GetStatusAsync().Result;

            Console.WriteLine($"未読のある部屋の数:{myStatus.unread_room_num}, 返信のある部屋の数:{myStatus.mention_room_num}, タスクのある部屋の数:{myStatus.mytask_room_num}"
                              + $", 未読の数:{myStatus.unread_num}, 返信の数:{myStatus.mention_num}, タスクの数:{myStatus.mention_num}");
        }
    }
}
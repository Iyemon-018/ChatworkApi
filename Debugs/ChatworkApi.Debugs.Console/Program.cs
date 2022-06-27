namespace ChatworkApi.Debugs.Console;

using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

internal class Program
{
    private static readonly string LogFileName =
            Path.Combine(Environment.CurrentDirectory, $"{DateTime.Now:yyyy-MM-dd_HHmmss}.log");

    private static StreamWriter _logWriter;

    private static void Main(string[] args)
    {
        var apiToken = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "api-token.txt"));
        var client   = new Client(new HttpClient());
        client.ApiToken(apiToken);

        try
        {
            using (_logWriter = new StreamWriter(LogFileName, true, Encoding.UTF8))
            {
                ClientDebug(client);
            }
        }
        catch (Exception e)
        {
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, $"Error_{DateTime.Now:yyyy-MM-dd_HHmmss}.log")
                  , $"{e.Message}{Environment.NewLine}"
                  + $"{e.StackTrace}");
        }
    }

    private static void ClientDebug(IClient client)
    {
        int myRoomId;

        {
            var response = client.Me.GetMeAsync().Result;

            WriteLine($"RateLimit > {response.RateLimit}");

            var me = response.Content;
            myRoomId = me.room_id;

            WriteLine($"所属:{me.organization_name}, 氏名:{me.name}, ID:{me.account_id}");
        }
        {
            var myStatus = client.My.GetMyStatusAsync().Result.Content;

            WriteLine($"未読のある部屋の数:{myStatus.unread_room_num}, 返信のある部屋の数:{myStatus.mention_room_num}, タスクのある部屋の数:{myStatus.mytask_room_num}"
                    + $", 未読の数:{myStatus.unread_num}, 返信の数:{myStatus.mention_num}, タスクの数:{myStatus.mention_num}");
        }
        {
            var works = client.My.GetMyTasksAsync(null, TaskStatus.Open).Result.Content;
            WriteLine("- 自分に割り当てられた未完了タスク一覧");
            WriteLine($"{string.Join(Environment.NewLine, works.Select(x => $"{x.limit_type}-{x.limit_time}{Environment.NewLine}{x.assigned_by_account.name}, {x.room.name}{Environment.NewLine}{x.body}"))}");
        }
        {
            var contacts = client.Contacts
                                 .GetContactsAsync()
                                 .Result
                                 .Content
                                 .OrderBy(x => x.account_id)
                                 .Select(x => $"{x.organization_name}:{x.name}[{x.department}]")
                                 .ToArray();

            WriteLine("- 自分のコンタクト一覧");
            WriteLine($"{string.Join(Environment.NewLine, contacts)}");
        }

        {
            var myRooms = client.Rooms
                                .GetMyRoomsAsync()
                                .Result
                                .Content
                                .OrderBy(x => x.last_update_time)
                                .Select(x => $"{x.name}:{x.room_id}(未読:{x.unread_num}/返信:{x.mention_num}/タスク:{x.mytask_num})[最終更新日時:{x.last_update_time:yyyy/MM/dd HH:mm:ss}]")
                                .ToArray();

            WriteLine($"- 自分の参加しているグループチャットの一覧");
            WriteLine($"{string.Join(Environment.NewLine, myRooms)}");
        }
        {
            var roomConfiguration = client.Rooms.GetRoomConfigurationAsync(myRoomId).Result.Content;

            WriteLine($"- マイチャットの構成");
            WriteLine($"{roomConfiguration.name}:{roomConfiguration.room_id}(タスク:{roomConfiguration.mytask_num})[最終更新日時:{roomConfiguration.last_update_time:yyyy/MM/dd HH:mm:ss}]");
        }
        {
            var myRoomMessage = client.Rooms.AddMessageAsync(myRoomId, $"{DateTime.Now:yyyy/MM/dd(ddd) HH:mm:ss.fff} Test from ChatworkApi.Debugs.Console.", false).Result.Content;
            WriteLine($"- マイチャットへメッセージ送信");
        }
        {
            var roomId = int.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "room_id.txt")));
            var roomMembers = client.Rooms
                                    .GetRoomMembersAsync(roomId)
                                    .Result
                                    .Content
                                    .OrderBy(x => x.account_id)
                                    .Select(x => $"{x.name}({x.account_id}) - {x.organization_name}")
                                    .ToArray();

            WriteLine($"- グループチャットのメンバー一覧");
            WriteLine($"{string.Join(Environment.NewLine, roomMembers)}");
        }
        {
            var messages = client.Rooms
                                 .GetMessagesAsync(myRoomId, true)
                                 .Result
                                 .Content
                                 .Select(x => $"{x.send_time:yyyy/MM/dd HH:mm:ss} - {x.account.name}:{x.body}")
                                 .ToArray();

            WriteLine($"- マイチャットのメッセージ一覧");
            WriteLine($"{string.Join(Environment.NewLine, messages)}");
        }
    }
    private static void WriteLine(string message)
    {
        _logWriter.WriteLine(message);
        Console.WriteLine(message);
    }
}
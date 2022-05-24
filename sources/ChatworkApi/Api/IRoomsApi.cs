namespace ChatworkApi.Api;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models;
using TaskStatus = ChatworkApi.TaskStatus;

/// <summary>
/// /room API 機能を提供するためのインターフェースです。
/// </summary>
/// <remarks>
/// http://developer.chatwork.com/ja/endpoint_rooms.html#GET-rooms
/// </remarks>
public interface IRoomsApi
{
    /// <summary>
    /// 自分のチャット一覧を非同期で取得します。
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>自分のチャット情報を全て返します。</returns>
    Task<ChatworkResponse<IEnumerable<MyRoom>>> GetMyRoomsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// グループチャットを新規作成します。
    /// </summary>
    /// <param name="name">作成したいグループチャットの名称</param>
    /// <param name="adminMembersIds">管理者権限を持つユーザーのIDリスト</param>
    /// <param name="memberMembersIds">メンバー権限を持つユーザーのIDリスト</param>
    /// <param name="readonlyMembersIds">閲覧権限のみ持つユーザーのIDリスト</param>
    /// <param name="description">グループチャットに表示する概要説明</param>
    /// <param name="iconType">グループチャット アイコンの種類</param>
    /// <param name="link">招待リンクを作成するかどうか（<c>true</c> の場合、作成します。<c>false</c> もしくは <c>null</c> の場合、作成しません。）</param>
    /// <param name="linkCode">リンクのパス部分の文字列（省略した場合はランダムな文字列となります。）</param>
    /// <param name="needLinkAcceptance">作成したグループチャットに参加する場合は管理者の承認を必要とするかどうか（<c>true</c> の場合、承認が必要になります。<c>false</c> もしくは、<c>null</c> の場合、承認は不要となります。）</param>
    /// <param name="cancellationToken"></param>
    /// <returns>作成したグループチャットの ID を返します。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="name"/> が <c>null</c> もしくは <c>string.Empty</c> の場合にスローされます。</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="adminMembersIds"/> の要素数が <c>1</c> 未満の場合にスローされます。</exception>
    Task<ChatworkResponse<CreatedRoom>> CreateNewRoomAsync(string             name
                                                         , int[]              adminMembersIds
                                                         , int[]              memberMembersIds   = default
                                                         , int[]              readonlyMembersIds = default
                                                         , string             description        = default
                                                         , GroupChatIconType? iconType           = default
                                                         , bool?              link               = default
                                                         , string             linkCode           = default
                                                         , bool?              needLinkAcceptance = default
                                                         , CancellationToken  cancellationToken  = default);

    /// <summary>
    /// 指定したグループチャットの構成を取得します。
    /// </summary>
    /// <param name="roomId">取得するグループチャットのID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>グループチャットの構成情報を返します。</returns>
    Task<ChatworkResponse<RoomConfiguration>> GetRoomConfigurationAsync(int roomId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したグループチャットの構成を更新します。
    /// </summary>
    /// <param name="roomId">更新したいグループチャットのID</param>
    /// <param name="name">グループチャットの名称</param>
    /// <param name="description">グループチャットの概要説明</param>
    /// <param name="iconType">グループチャットに表示するアイコンの種類</param>
    /// <param name="cancellationToken"></param>
    /// <returns>更新したグループチャットの構成を取得します。</returns>
    Task<ChatworkResponse<UpdatedRoomConfiguration>> UpdateRoomConfigurationAsync(int                roomId
                                                                                , string             name              = default
                                                                                , string             description       = default
                                                                                , GroupChatIconType? iconType          = default
                                                                                , CancellationToken  cancellationToken = default);

    /// <summary>
    /// 指定したグループチャットから退室します。
    /// </summary>
    /// <param name="roomId">退室するグループチャットのID</param>
    /// <param name="action">
    ///     退室する際のアクションを指定します。<br/>
    ///     <see cref="LeavingRoomAction.Leave"/> を選択すると、グループチャット内にある自分が担当のタスク、及び自分が送信したファイルは削除されます。
    ///     <see cref="LeavingRoomAction.Delete"/> を選択すると、グループチャットに参加しているメンバー全員のメッセージ、タスク、ファイルがすべて削除されます。
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>非同期タスクを返します。</returns>
    Task<ChatworkResponse<EmptyResponse>> LeavingRoomAsync(int               roomId
                                                         , LeavingRoomAction action
                                                         , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したグループチャットに参加しているメンバーの情報を取得します。
    /// </summary>
    /// <param name="roomId">取得対象のグループチャットのID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>グループチャットに参加しているメンバーの情報シーケンスを返します。</returns>
    Task<ChatworkResponse<IEnumerable<RoomMember>>> GetRoomMembersAsync(int roomId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したグループチャットのメンバーを更新します。
    /// </summary>
    /// <param name="roomId">変更対象のグループチャットのID</param>
    /// <param name="adminMembersIds">管理者権限に設定するユーザーのIDリスト</param>
    /// <param name="memberMembersIds">メンバー権限を持つユーザーのIDリスト</param>
    /// <param name="readonlyMembersIds">読み取り専用権限のみ持つユーザーのIDリスト</param>
    /// <param name="cancellationToken"></param>
    /// <returns>メンバーの更新結果を返します。</returns>
    Task<ChatworkResponse<UpdatedRoomMember>> UpdateRoomMembersAsync(int               roomId
                                                                   , int[]             adminMembersIds
                                                                   , int[]             memberMembersIds   = default
                                                                   , int[]             readonlyMembersIds = default
                                                                   , CancellationToken cancellationToken  = default);

    /// <summary>
    /// 指定したグループチャットのメッセージを取得します。
    /// </summary>
    /// <param name="roomId">取得対象のグループチャットID</param>
    /// <param name="force">
    ///     最新の 100 件を取得するかどうか。
    ///     <c>true</c> の場合、最新の 100 件を取得します。
    ///     <c>false</c> もしくは <c>null</c> の場合、前回取得分からの差分を取得します。
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>最大 100 件までのメッセージを取得します。</returns>
    Task<ChatworkResponse<IEnumerable<Message>>> GetMessagesAsync(int               roomId
                                                                , bool?             force
                                                                , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したグループチャットにメッセージを追加します。
    /// </summary>
    /// <param name="roomId">追加するグループチャットのID</param>
    /// <param name="body">
    ///     メッセージの本文<br/>
    ///     http://developer.chatwork.com/ja/messagenotation.html
    /// </param>
    /// <param name="unread">
    ///     メッセージを未読とするかどうか。<c>true</c> の場合、未読にします。<c>false</c> もしくは、<c>null</c> の場合、既読にします。
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>メッセージを追加したグループチャットの構成情報を返します。</returns>
    Task<ChatworkResponse<AddMessage>> AddMessageAsync(int               roomId
                                                     , string            body
                                                     , bool              unread
                                                     , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したメッセージを既読にします。
    /// </summary>
    /// <param name="roomId">ルームID</param>
    /// <param name="messageId">既読にするメッセージID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>既読にしたメッセージ情報を返します。</returns>
    Task<ChatworkResponse<ReadMessage>> ReadMessageAsync(int               roomId
                                                       , string            messageId
                                                       , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したメッセージを未読にします。
    /// </summary>
    /// <param name="roomId">ルームID</param>
    /// <param name="messageId">未読にするメッセージID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>未読にしたメッセージ情報を返します。</returns>
    Task<ChatworkResponse<UnreadMessage>> UnreadMessageAsync(int               roomId
                                                           , string            messageId
                                                           , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したルームのメッセージ情報を取得します。
    /// </summary>
    /// <param name="roomId">ルームID</param>
    /// <param name="messageId">取得したいメッセージID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>メッセージ情報を返します。</returns>
    Task<ChatworkResponse<Message>> GetMessageAsync(int               roomId
                                                  , string            messageId
                                                  , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したメッセージ本文を更新します。
    /// </summary>
    /// <param name="roomId">ルームID</param>
    /// <param name="messageId">更新したいメッセージID</param>
    /// <param name="body">更新するメッセージ本文</param>
    /// <param name="cancellationToken"></param>
    /// <returns>更新したメッセージ情報を返します。</returns>
    Task<ChatworkResponse<UpdatedMessage>> UpdateMessageAsync(int               roomId
                                                            , string            messageId
                                                            , string            body
                                                            , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したメッセージを削除します。
    /// </summary>
    /// <param name="roomId">ルームID</param>
    /// <param name="messageId">削除したいメッセージID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>削除したメッセージ情報を返します。</returns>
    Task<ChatworkResponse<DeletedMessage>> DeleteMessageAsync(int               roomId
                                                            , string            messageId
                                                            , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したグループチャットのタスク一覧を取得します。
    /// </summary>
    /// <param name="roomId">取得対象のグループチャットID</param>
    /// <param name="accountId">タスクの担当者に設定されているアカウントID</param>
    /// <param name="assignedByAccountId">タスクの依頼者のアカウントID</param>
    /// <param name="status">取得するタスクのステータス</param>
    /// <param name="cancellationToken"></param>
    /// <returns>タスクの一覧を返します。</returns>
    Task<ChatworkResponse<IEnumerable<RoomTask>>> GetRoomTasksAsync(int               roomId
                                                                  , int?              accountId           = default
                                                                  , int?              assignedByAccountId = default
                                                                  , TaskStatus?       status              = default
                                                                  , CancellationToken cancellationToken   = default);

    /// <summary>
    /// 指定したグループチャットにタスクを追加します。
    /// </summary>
    /// <param name="roomId">追加対象のグループチャットID</param>
    /// <param name="body">タスクの内容</param>
    /// <param name="assignToIds">タスクの担当者のアカウントIDリスト</param>
    /// <param name="limitType">タスクの期限種別</param>
    /// <param name="limit">タスクの期限</param>
    /// <param name="cancellationToken"></param>
    /// <returns>追加したタスクの情報を返します。</returns>
    Task<ChatworkResponse<AddTask>> AddTaskAsync(int               roomId
                                               , string            body
                                               , int[]             assignToIds
                                               , TaskLimitType?    limitType         = default
                                               , DateTime?         limit             = default
                                               , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したグループチャットのタスク情報を取得します。
    /// </summary>
    /// <param name="roomId">取得対象のグループチャットのID</param>
    /// <param name="taskId">取得したいタスクのID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>タスク情報を返します。</returns>
    Task<ChatworkResponse<RoomTask>> GetRoomTaskAsync(int               roomId
                                                    , int               taskId
                                                    , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したグループチャットのタスクの状態を更新します。
    /// </summary>
    /// <param name="roomId">更新対象のタスクのあるグループチャットのID</param>
    /// <param name="taskId">更新対象のタスクID</param>
    /// <param name="status">更新後のタスク状態</param>
    /// <param name="cancellationToken"></param>
    /// <returns>更新したタスク情報を返します。</returns>
    Task<ChatworkResponse<UpdatedTaskStatus>> UpdateTaskStatusAsync(int               roomId
                                                                  , int               taskId
                                                                  , TaskStatus        status
                                                                  , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したチャット ルーム内のファイル一覧を取得します。
    /// </summary>
    /// <param name="roomId">ルームID</param>
    /// <param name="accountId">取得したいファイルをアップロードしたユーザーのアカウントID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>ファイル情報を取得します。</returns>
    Task<ChatworkResponse<IEnumerable<FileData>>> GetFilesAsync(int               roomId
                                                              , int               accountId
                                                              , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定したチャット ルームにファイルをアップロードします。
    /// </summary>
    /// <param name="roomId">ルームID</param>
    /// <param name="file">アップロードするファイルパス</param>
    /// <param name="message">追加するコメント メッセージ</param>
    /// <param name="cancellationToken"></param>
    /// <returns>アップロードしたファイル情報を返します。；</returns>
    Task<ChatworkResponse<UploadedFileData>> UploadFileAsync(int               roomId
                                                           , string            file
                                                           , string            message
                                                           , CancellationToken cancellationToken = default);

    /// <summary>
    /// ファイル情報を取得するための情報を取得します。
    /// </summary>
    /// <param name="roomId">ルームID</param>
    /// <param name="fileId">ファイルID</param>
    /// <param name="createDownloadUrl">
    ///     30秒間だけダウンロード可能なURLを生成するかどうか。
    ///     <c>true</c>の場合、URLを生成します。
    ///     <c>false</c> もしくは<c>null</c> の場合、URLは生成しません。
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>ファイル情報を返します。</returns>
    Task<ChatworkResponse<FileData>> GetFileAsync(int               roomId
                                                , int               fileId
                                                , bool?             createDownloadUrl
                                                , CancellationToken cancellationToken = default);

    /// <summary>
    /// 招待リンクを取得します。
    /// </summary>
    /// <param name="roomId">ルームID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>招待リンク情報を取得します。</returns>
    Task<ChatworkResponse<InviteLink>> GetInviteLink(int roomId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 招待リンクを作成します。
    /// </summary>
    /// <param name="roomId">ルームID</param>
    /// <param name="code">リンクのパス部分に該当する文字列。<c>null</c> もしくは、<c>string.Empty</c> の場合、ランダムな文字列になります。</param>
    /// <param name="description">リンクページに表示される説明文です。</param>
    /// <param name="needAcceptance">
    ///     参加に管理者の承認を必要とするかどうか。
    ///     <c>true</c> の場合、承認が必要になります。
    ///     <c>false</c> もしくは <c>null</c> の場合、承認は不要になります。
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>招待リンクの情報を返します。</returns>
    Task<ChatworkResponse<InviteLink>> CreateInviteLink(int               roomId
                                                      , string            code              = default
                                                      , string            description       = default
                                                      , bool?             needAcceptance    = default
                                                      , CancellationToken cancellationToken = default);

    /// <summary>
    /// 招待リンクを更新します。
    /// </summary>
    /// <param name="roomId">ルームID</param>
    /// <param name="code">リンクのパス部分に該当する文字列。<c>null</c> もしくは、<c>string.Empty</c> の場合、ランダムな文字列になります。</param>
    /// <param name="description">リンクページに表示される説明文です。</param>
    /// <param name="needAcceptance">
    ///     参加に管理者の承認を必要とするかどうか。
    ///     <c>true</c> の場合、承認が必要になります。
    ///     <c>false</c> もしくは <c>null</c> の場合、承認は不要になります。
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>更新した招待リンクの情報を返します。</returns>
    Task<ChatworkResponse<InviteLink>> UpdateInviteLink(int               roomId
                                                      , string            code
                                                      , string            description
                                                      , bool?             needAcceptance
                                                      , CancellationToken cancellationToken = default);

    /// <summary>
    /// 指定した招待リンクを削除します。
    /// </summary>
    /// <param name="roomId">ルームID</param>
    /// <param name="cancellationToken"></param>
    /// <returns>削除した招待リンクの情報を返します。</returns>
    Task<ChatworkResponse<DeletedInviteLink>> DeleteInviteLink(int roomId, CancellationToken cancellationToken = default);
}
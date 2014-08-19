using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ChatR.Model;
using Microsoft.AspNet.SignalR;

namespace ChatR.Server.Hubs
{
    public class ChatHub : Hub<IChatHubClient>
    {
        public void SendMessage(ChatMessage message)
        {
            var user = _connectedUsers.First(m => m.ConnectionId == Context.ConnectionId);

            Clients.Others.OnMessageReceived(user, message);
        }

        public void SendMessage(string username, ChatMessage message)
        {
            var user = _connectedUsers.First(m => m.ConnectionId == Context.ConnectionId);

            var receiver = _connectedUsers.First(m => m.UserName == username);

            Clients.Client(receiver.ConnectionId).OnMessageReceived(user, message);
        }

        public string Login(string userName)
        {
            var id = Context.ConnectionId;

            if (_connectedUsers.All(x => x.ConnectionId != id))
            {
                var userDetail = new UserDetail { ConnectionId = id, UserName = userName };
                _connectedUsers.Add(userDetail);

                Clients.Caller.OnConnected(userDetail, _connectedUsers.ToArray());

                Clients.Others.OnNewUserConnected(userDetail);
            }
            return userName;
        }

        public GroupInfo AddOrJoinGroup(string groupName)
        {
            var id = Context.ConnectionId;

            return new GroupInfo
            {
                GroupName = groupName,
                Users = { _connectedUsers.First(u => u.ConnectionId == id) }
            };
        }

        static readonly ConcurrentBag<UserDetail> _connectedUsers = new ConcurrentBag<UserDetail>();

        static readonly ConcurrentBag<GroupInfo> _Groups = new ConcurrentBag<GroupInfo>();
    }
}
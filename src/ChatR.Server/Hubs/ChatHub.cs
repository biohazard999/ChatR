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

        public void Login(string userName)
        {
            var id = Context.ConnectionId;

            if (_connectedUsers.All(x => x.ConnectionId != id))
            {
                var userDetail = new UserDetail {ConnectionId = id, UserName = userName};
                _connectedUsers.Add(userDetail);

                Clients.Caller.OnConnected(userDetail, _connectedUsers.ToArray());

                Clients.Others.OnNewUserConnected(userDetail);
            }
        }

        readonly List<UserDetail> _connectedUsers = new List<UserDetail>();
    }
}
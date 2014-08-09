using System.Collections.Generic;
using ChatR.Model;

namespace ChatR.Server.Hubs
{
    public interface IChatHubClient
    {
        void OnMessageReceived(UserDetail sender, ChatMessage message);
        void OnConnected(UserDetail userDetail, UserDetail[] connectedUsers);
        void OnNewUserConnected(UserDetail userDetail);
    }
}
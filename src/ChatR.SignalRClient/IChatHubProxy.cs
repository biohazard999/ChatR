using System;
using System.Threading.Tasks;
using ChatR.Model;

namespace ChatR.SignalRClient
{
    public interface IChatHubProxy
    {
        Task<bool> Connect();
        bool IsConnected { get; }
        Action<UserDetail, ChatMessage> MessageReceived { get; set; }
        Action<UserDetail, UserDetail[]> Connected { get; set; }
        Action<UserDetail> NewUserConnected { get; set; }
        Task<ChatMessage> SendMessage(ChatMessage message);
        Task<string> Login(string userName);
    }
}
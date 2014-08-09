using System;
using System.Threading.Tasks;
using ChatR.Model;

namespace ChatR.SignalRClient
{
    public interface IChatHubProxy
    {
        Task<bool> Connect();
        bool IsConnected { get; }
        Action<ChatMessage> MessageReceived { get; set; }
        Task<ChatMessage> SendMessage(ChatMessage message);
    }
}
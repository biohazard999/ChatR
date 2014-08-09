using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatR.Model;
using Microsoft.AspNet.SignalR.Client;

namespace ChatR.SignalRClient
{
    public class ChatHubProxy : IChatHubProxy
    {
        private readonly HubConnection _connection;
        private readonly SynchronizationContext _context;
        private readonly IHubProxy _proxy;

        public bool IsConnected { get; private set; }

        public ChatHubProxy(HubConnection connection, SynchronizationContext context)
        {
            _connection = connection;
            _context = context;
            _proxy = _connection.CreateHubProxy("ChatHub");

            _proxy.On<ChatMessage>("MessageReceived", OnMessageReceived);
            MessageReceived = m => { };
        }

        public async Task<bool> Connect()
        {
            if (IsConnected)
                return true;

            await _connection.Start();

            IsConnected = true;

            return true;
        }

        private void OnMessageReceived(ChatMessage args)
        {
            _context.Post(_ => MessageReceived(args), null);
        }

        public Action<ChatMessage> MessageReceived { get; set; }
        
        public Task<ChatMessage> SendMessage(ChatMessage message)
        {
            return _proxy.Invoke<ChatMessage>("SendMessage", message);
        }
    }
}

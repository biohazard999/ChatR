using System;
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

            _proxy.On<UserDetail, ChatMessage>("MessageReceived", OnMessageReceived);
            MessageReceived = (sender, message) => { };

            _proxy.On<UserDetail, UserDetail[]>("OnConnected", OnConnected);
            Connected = (userdetail, userdetails) => { };
        }
        
        public async Task<bool> Connect()
        {
            if (IsConnected)
                return true;

            await _connection.Start();

            IsConnected = true;

            return true;
        }

        #region ClientMethods

        private void OnMessageReceived(UserDetail sender, ChatMessage message)
        {
            _context.Post(_ => MessageReceived(sender, message), null);
        }

        public Action<UserDetail, ChatMessage> MessageReceived { get; set; }

        private void OnConnected(UserDetail userDetail, UserDetail[] userDetails)
        {
            _context.Post(_ => Connected(userDetail, userDetails), null);
        }

        public Action<UserDetail, UserDetail[]> Connected { get; set; }

        #endregion

        #region ServerMethods

        public Task<ChatMessage> SendMessage(ChatMessage message)
        {
            return _proxy.Invoke<ChatMessage>("SendMessage", message);
        }

        public Task<string> Login(string userName)
        {
            return _proxy.Invoke<string>("Login", userName);
        }

        #endregion
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using ChatR.Model;
using Microsoft.AspNet.SignalR.Client;

namespace ChatR.SignalRClient
{
    public class ChatHubProxy : IChatHubProxy, IDisposable
    {
        private HubConnection _connection;
        private readonly SynchronizationContext _context;
        private readonly IHubProxy _proxy;
        

        public bool IsConnected { get; private set; }

        public ChatHubProxy(HubConnection connection, SynchronizationContext context)
        {
            _connection = connection;
            _context = context;
            _proxy = _connection.CreateHubProxy("ChatHub");

            _proxy.On<UserDetail, ChatMessage>("OnMessageReceived", OnMessageReceived);
            MessageReceived = (sender, message) => { };

            _proxy.On<UserDetail, UserDetail[]>("OnConnected", OnConnected);
            Connected = (userdetail, userdetails) => { };

            _proxy.On<UserDetail>("OnNewUserConnected", OnNewUserConnected);
            NewUserConnected = (userdetail) => { };
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
        
        private void OnNewUserConnected(UserDetail userDetail)
        {
            _context.Post(_ => NewUserConnected(userDetail), null);
        }
        public Action<UserDetail> NewUserConnected { get; set; }

        #endregion

        #region ServerMethods

        public Task<ChatMessage> SendMessage(ChatMessage message)
        {
            return _proxy.Invoke<ChatMessage>("SendMessage", message);
        }
        
        public Task<object> SendMessage(string userName, ChatMessage chatMessage)
        {
            return _proxy.Invoke<object>("SendMessage", userName, chatMessage);
        }

        public Task<string> Login(string userName)
        {
            return _proxy.Invoke<string>("Login", userName);
        }

        public Task<GroupInfo> AddOrJoinGroup(string groupName)
        {
            return _proxy.Invoke<GroupInfo>("AddOrJoinGroup", groupName);
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}

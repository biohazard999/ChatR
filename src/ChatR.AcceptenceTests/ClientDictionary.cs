using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatR.SignalRClient;
using ChatR.TestUtils;
using Microsoft.AspNet.SignalR.Client;

namespace ChatR.AcceptenceTests
{
    public sealed class ClientDictionary : IDisposable
    {
        private readonly Dictionary<string, ChatHubProxy> _clients = new Dictionary<string, ChatHubProxy>();
        public ClientDictionary(string uri, IEnumerable<string> clients)
        {
            var syncContext = new TestSynchronizationContext();
            foreach (var clientLogin in clients)
            {
                var client = new ChatHubProxy(new HubConnection(uri), syncContext);

                _clients[clientLogin] = client;
            }
        }

        public ChatHubProxy this[string key]
        {
            get { return _clients[key]; }
        }

        public async Task<ClientDictionary> InitializeClients()
        {
            foreach (var chatHubProxy in _clients)
            {
                await chatHubProxy.Value.Connect();
                await chatHubProxy.Value.Login(chatHubProxy.Key);
            }
            return this;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var chatHubProxy in _clients)
                {
                    chatHubProxy.Value.Dispose();
                }
                _clients.Clear();
            }
        }
    }
}
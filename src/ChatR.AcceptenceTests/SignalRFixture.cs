using System;
using ChatR.Server;

namespace ChatR.AcceptenceTests
{
    public class SignalRFixture : IDisposable
    {

        private IDisposable app;

        public string uri = "http://localhost:5002";

        public SignalRFixture()
        {
            app = Microsoft.Owin.Hosting.WebApp.Start(uri, builder => new WebPipeline().Configuration(builder));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && app != null)
            {
                app.Dispose();
                app = null;
            }
        }
    }
}
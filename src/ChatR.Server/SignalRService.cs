using System;
using System.Configuration;
using System.Linq;
using Microsoft.Owin.Hosting;

namespace ChatR.Server
{
    public class SignalRService
    {
        private IDisposable WebApplication { get; set; }

        public void Start()
        {
            var host = "http://localhost:5000";
            if (ConfigurationManager.AppSettings.Keys.OfType<string>().Any(m => m == "Url"))
                host = ConfigurationManager.AppSettings["Url"];

            WebApplication = WebApp.Start<WebPipeline>(host);
        }
        
        public void Stop()
        {
            WebApplication.Dispose();
            WebApplication = null;
        }
    }
}
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace ChatR.Server
{
    public class WebPipeline
    {
        public void Configuration(IAppBuilder application)
        {
            UseSignalR(application);
        }

        private void UseSignalR(IAppBuilder application)
        {
            application.UseCors(CorsOptions.AllowAll);

            application.MapSignalR(new HubConfiguration
            {
                EnableDetailedErrors = true,
                EnableJSONP = false,
                EnableJavaScriptProxies = true,
            });
        }
    }
}
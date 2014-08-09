using Microsoft.AspNet.SignalR;
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
            application.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            application.MapSignalR(new HubConfiguration()
            {
                EnableDetailedErrors = true,
                EnableJSONP = false,
                EnableJavaScriptProxies = true,
            });
        }
    }
}
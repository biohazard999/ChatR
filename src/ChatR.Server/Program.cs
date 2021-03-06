﻿using Topshelf;

namespace ChatR.Server
{
    static class Program
    {
        static int Main()
        {
            var exitCode = HostFactory.Run(c =>
            {
                c.SetDescription("ChatR.Server is the main server for communicating with ChatR.Clients");
                c.SetDisplayName("ChatR.Server");
                c.SetServiceName("ChatR.Server");
                c.RunAsLocalSystem();
                c.StartAutomatically();
                
                c.Service<SignalRService>(service =>
                {
                    service.ConstructUsing(() => new SignalRService());

                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });

            });

            return (int) exitCode;
        }
    }
}

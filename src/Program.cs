﻿using BeetleX.FastHttpApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BeetleX.WebApiTester
{
    class Program
    {


        static void Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<HttpServerHosted>();
                });
            builder.Build().Run();
        }


        public class HttpServerHosted : IHostedService
        {
            private HttpApiServer mApiServer;

            public virtual Task StartAsync(CancellationToken cancellationToken)
            {
                mApiServer = new HttpApiServer();
                mApiServer.Register(typeof(Program).Assembly);
                mApiServer.Options.Debug = true;
                mApiServer.Open();

                mApiServer.Log(EventArgs.LogType.Info, $"Web api benchmark started[{typeof(Program).Assembly.GetName().Version}]");
                return Task.CompletedTask;
            }

            public virtual Task StopAsync(CancellationToken cancellationToken)
            {
                mApiServer.Dispose();
                return Task.CompletedTask;
            }
        }
    }
}

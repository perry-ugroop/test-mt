using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Mt.Common;
using Mt.Communication.Commands;

namespace Mt.Producer 
{
  public class Main 
  {
      public static void Run(IConfig config, List<string> parms) 
      {
          if(parms.Count == 0) {
              Console.WriteLine("Missing queue name");
              return;
          }

          Console.WriteLine("Producer is running using queue {0}...", parms[0]);

          var host = config.Get("host");
          var port = config.GetInt("port");
          var username = config.Get("username");
          var password = config.Get("password");
          var queue = parms[0];

          var bus = Bus.Factory.CreateUsingRabbitMq(cfg => {
              var mtHost = cfg.Host(new Uri($"rabbitmq://{host}:{port}"), hostConfig => {
                  hostConfig.Username(username);
                  hostConfig.Password(password);
              });

              /*
              cfg.ReceiveEndpoint(mtHost, queue, endpoint => {
                endpoint.Handler<CustMessage>(ctx => {
                    Task ret = Console.Out.WriteLineAsync($"Received #{ctx.Message.Id}: {ctx.Message.Text}");
                    return ret;
                });
              });
              */
          });

          // bus.Start();
      }
  }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Mt.Common;
using Mt.Communication.Commands;

namespace Mt.Consumer
{
  public class Main 
  {
      private static IBusControl bus;

      public static void Run(IConfig config, List<string> parms) 
      {
          Console.WriteLine("Consumer is running using queue '{0}', press Ctrl-C to stop...", parms[0]);

          // Intercept Ctrl-C
          Console.CancelKeyPress += new ConsoleCancelEventHandler(CtrlCHandler);

          var host = config.Get("host");
          var port = config.GetInt("port");
          var username = config.Get("username");
          var password = config.Get("password");
          var queue = parms[0];

          bus = CreateBus(host, port, username, password, queue);
          bus.Start();
      }
      private static IBusControl CreateBus(string host, int port, string username, string password, string queue)
      {
          IBusControl bus = Bus.Factory.CreateUsingRabbitMq(cfg => {
              var mtHost = cfg.Host(new Uri($"rabbitmq://{host}:{port}"), hostConfig => {
                  hostConfig.Username(username);
                  hostConfig.Password(password);
              });

              cfg.ReceiveEndpoint(mtHost, queue, endpoint => {
                  // Specify endpoint consumers
                  endpoint.Consumer<MessageConsumer>() ;
              });
          });

          return bus;
      }

      private static void CtrlCHandler(object sender, ConsoleCancelEventArgs e)
      {
          Console.WriteLine("");
          Console.WriteLine("Exiting consumer...");
          bus.Stop();
      }
  }
}
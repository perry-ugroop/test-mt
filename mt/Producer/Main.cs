using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Mt.Common;
using Mt.Communication.Events;

namespace Mt.Producer 
{
  public class Main 
  {
      private static IBusControl bus;

      public static async Task Run(IConfig config, List<string> parms) 
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

          bus = CreateBus(host, port, username, password, queue);
          bus.Start();

          bool quit = false;
          while(!quit)
          {
             var msg = GetMessageFromUser();

             Console.WriteLine("Sending message...");
             Console.WriteLine("> {0}: '{1}'", msg.Author, msg.Text);

             await bus.Publish<IMessageAdded>(msg);

             quit = AskUserIfQuit();
          }

          bus.Stop();
      }

      private static IMessageAdded GetMessageFromUser()
      {
          Console.Write("Name: ");
          string author = Console.ReadLine();

          Console.Write("Message: ");
          string message = Console.ReadLine();

          var msg = new ConcreteMessageAdded
          {
             Id = GenerateId(),
             Author = author,
             Text = message, 
          };

          return msg;
      }

      private static bool AskUserIfQuit()
      {
          bool quit = false;

          Console.WriteLine("");
          Console.Write("Quit (y/n)? ");

          var key = Console.ReadKey();
          Console.WriteLine("");

          if (key.Key == ConsoleKey.Y)
          {
            quit = true;
          }

          Console.WriteLine("");

          return quit;
      }

      private static int GenerateId()
      {
          return (int) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
      }

      private static IBusControl CreateBus(string host, int port, string username, string password, string queue)
      {
          IBusControl bus = Bus.Factory.CreateUsingRabbitMq(cfg => {
              var mtHost = cfg.Host(new Uri($"rabbitmq://{host}:{port}"), hostConfig => {
                  hostConfig.Username(username);
                  hostConfig.Password(password);
              });
          });

          return bus;
      }
  }

    class ConcreteMessageAdded : IMessageAdded
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Author { get; set; }
    }
}
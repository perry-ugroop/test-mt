using System;
using Common;
using Producer;
using Consumer;
using System.IO;
using System.Linq;

namespace masstransit
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0) 
            {
                return;
            }

            var config = Utils.GetConfiguration("rabbitmq");
            var parms = args.Skip(1).ToList();

            if(args[0] == "producer")
            {
               Producer.Main.Run(config, parms);
            }
            else if(args[0] == "consumer")
            {
               Consumer.Main.Run(config, parms);
            }
        }
    }
}

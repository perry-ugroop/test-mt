using System;
using System.Threading.Tasks;
using MassTransit;
using Mt.Communication.Commands;

namespace Mt.Consumer
{
    public class MessageConsumer : IConsumer<IAddMessage>
    {
        public async Task Consume(ConsumeContext<IAddMessage> context)
        {
            await Console.Out.WriteLineAsync("Debug");
        }
    }
}
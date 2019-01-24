using System;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Mt.Communication.Events;

namespace Mt.Consumer
{
    public class MessageConsumer : IConsumer<IMessageAdded>
    {
        public async Task Consume(ConsumeContext<IMessageAdded> context)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Got the following message (#{context.Message.Id}) from {context.Message.Author}:");
            sb.AppendLine(context.Message.Text);
            sb.AppendLine("");
            
            await Console.Out.WriteLineAsync(sb.ToString());
        }
    }
}
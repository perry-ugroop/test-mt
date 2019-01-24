using System;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Mt.Communication.Commands;

namespace Mt.Consumer
{
    public class MessageConsumer : IConsumer<IAddMessage>
    {
        public async Task Consume(ConsumeContext<IAddMessage> context)
        {
            var sb = new StringBuilder();

            sb.AppendLine(String.Format("Got the following message (#{0}) from {1}:",
                context.Message.Id,
                context.Message.Author
              ));

            sb.AppendLine(context.Message.Text);
            sb.AppendLine("");
            
            await Console.Out.WriteLineAsync(sb.ToString());
        }
    }
}
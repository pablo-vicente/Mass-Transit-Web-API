using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mass_Transit
{
    class ConsumerMessage:IConsumer<MessageText>
    {
        async Task IConsumer<MessageText>.Consume(ConsumeContext<MessageText> context)
        {
            Console.Out.WriteLine($"Message Received: {context.Message.Text}");
        }
    }
}

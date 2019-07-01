using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiMassTransit.Models
{
    public class Consumer : IConsumer<MessageText>
    {
        public async Task Consume(ConsumeContext<MessageText> context)
        {
            //ISendEndpoint x = await context.GetSendEndpoint(new Uri("rabbitmq://localhost/"));
            //rabbitmq://localhost/my_queue
            await context.RespondAsync<MessageText>(new
            {
                Text = $"Received: {context.Message.Text}"
            });
        }
    }
}

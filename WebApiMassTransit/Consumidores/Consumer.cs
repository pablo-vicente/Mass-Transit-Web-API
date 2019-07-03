using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Contract;

namespace WebApiMassTransit.Models
{
    public class Consumer : IConsumer<IMessageText>
    {
        public async Task Consume(ConsumeContext<IMessageText> context)
        {
            //ISendEndpoint x = await context.GetSendEndpoint(new Uri("rabbitmq://localhost/"));
            //rabbitmq://localhost/my_queue
            //await context.RespondAsync<MessageText>(new
            //{
            //    Text = $"Received: {context.Message.Text}"
            //});

            string pathfile = @"C:\Users\Pablo\source\repos\Mass Transit WEB API\WebApiMassTransit\files";
            Directory.CreateDirectory(pathfile);

            pathfile = Path.Combine(pathfile, "consumer.txt");

            StreamWriter file = new StreamWriter(pathfile, true);
            await file.WriteLineAsync($"Message Received: {context.Message.Text}");
            file.Close();
        }
    }
}

using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Contract;
using Microsoft.Extensions.Hosting;

namespace WebApiMassTransit.Models
{
    public class ConsumerSaveMessageCommand : IConsumer<ISaveMessageCommand>
    {
        private readonly IBusControl _BusControl;

        public ConsumerSaveMessageCommand(IBusControl busControl)
        {
            _BusControl = busControl;
        }
        public async Task Consume(ConsumeContext<ISaveMessageCommand> context)
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
            await file.WriteLineAsync($"Message ID: {context.Message.id} Received: {context.Message.text}");
            file.Close();

            var endpoint = await _BusControl.GetSendEndpoint(new Uri("rabbitmq://localhost/ISaveConfirmedEvent"));

            //await endpoint.Send<ISaveConfirmedEvent>(new 
            //{
              //  id = context.Message.id,
                //text = context.Message.Text,
                //time = DateTime.Now
            //});

            await context.RespondAsync<ISaveConfirmedEvent>(new
            {
                id = context.Message.id,
                text = context.Message.text,
                time = DateTime.Now
            });


        }
    }
}

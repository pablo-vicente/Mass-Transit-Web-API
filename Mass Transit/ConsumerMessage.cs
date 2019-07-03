using Contract;
using MassTransit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mass_Transit
{
    class ConsumerMessage:IConsumer<IMessageText>
    {
        async Task IConsumer<IMessageText>.Consume(ConsumeContext<IMessageText> context)
        {
            //Console.Out.WriteLine($"Message Received: {context.Message.Text}");

            string pathfile = @"C:\Users\Pablo\source\repos\Mass Transit\Mass Transit\files";
            Directory.CreateDirectory(pathfile);

            pathfile = Path.Combine(pathfile, "consumer.txt");

            StreamWriter file = new StreamWriter(pathfile, true);
            await file.WriteLineAsync($"Message Received: {context.Message.Text}");
            file.Close();
        }
    }
}

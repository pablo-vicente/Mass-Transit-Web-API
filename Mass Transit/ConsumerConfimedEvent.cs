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
    class ConsumerConfimedEvent :IConsumer<ISaveConfirmedEvent>
    {
        async Task IConsumer<ISaveConfirmedEvent>.Consume(ConsumeContext<ISaveConfirmedEvent> context)
        {
            //Console.Out.WriteLine($"Message Received: {context.Message.Text}");

            string pathfile = @"C:\Users\Pablo\source\repos\Mass Transit WEB API\Mass Transit\files";
            Directory.CreateDirectory(pathfile);

            pathfile = Path.Combine(pathfile, "confirmedSaveMessage.txt");

            StreamWriter file = new StreamWriter(pathfile, true);
            string line =$"Confirmation ID: {context.Message.id} Received: {context.Message.time} Content: {context.Message.text}"; 
            //await file.WriteLineAsync($"Confirmed Save Message Received: {context.Message.time}");
            await file.WriteLineAsync(line);
            file.Close();
            System.Console.WriteLine(line);
        }
    }
}

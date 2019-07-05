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
    class ConsumerConfimedEvent : IConsumer<ISaveConfirmedEvent>
    {
        async Task IConsumer<ISaveConfirmedEvent>.Consume(ConsumeContext<ISaveConfirmedEvent> context)
        {
            string pathfile = @"/app/files";
            Directory.CreateDirectory(pathfile);

            pathfile = Path.Combine(pathfile, "confirmedSaveMessage.txt");

            StreamWriter file = new StreamWriter(pathfile, true);

            string line = $"Confirmation ID: {context.Message.id} Received: {context.Message.text} Content: {context.Message.text}\n\r";
            //await file.WriteLineAsync($"Confirmed Save Message Received: {context.Message.time}");
            await file.WriteLineAsync(line);
            file.Close();
            System.Console.WriteLine(line);
        }
    }
}
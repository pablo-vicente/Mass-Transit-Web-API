using Contract;
using MassTransit;
using System;
using System.IO;
using System.Threading;

namespace Mass_Transit
{
    public class MassTransit
    {
        public static void Main()
        {

            var bus = ConfigureBus();
            bus.Start();

            long i = 0;
            while(i<3000)
            {
                System.Console.WriteLine($"Salve in file: {i}");
                bus.Publish<ISaveMessageCommand>(new 
                { 
                    id = i,
                    text = $"Hello World, {i}"
                });
                i += 1;
                Thread.Sleep(150);
                System.Console.WriteLine("Waint 150....n");
            }

            //bus.Stop();
        }

        static IBusControl ConfigureBus()
        {
                      
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost/" ), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "ISaveConfirmedEvent", ep =>
                {
                    ep.PrefetchCount = 1;
                    ep.Consumer<ConsumerConfimedEvent>();
                });
            });
            
            return bus;
        }
    }
}

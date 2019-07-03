using Contract;
using MassTransit;
using System;
using System.IO;

namespace Mass_Transit
{
    public class MassTransit
    {
        public static void Main()
        {

            var bus = ConfigureBus();
            bus.Start();

            int i = 0;
            while(i<1000000)
            {
                var message = new MessageTextCommand() { Text = $"Hi {i}" };
                bus.Publish<IMessageText>(message);
                Console.WriteLine(i);
                i += 1;
            }

            bus.Stop();
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

                //sbc.ReceiveEndpoint(host, "textFile", ep =>
                //{
                //    ep.PrefetchCount = 1;
                //    ep.Consumer<ConsumerMessage>();
                //});
            });
            
            return bus;
        }
    }
}

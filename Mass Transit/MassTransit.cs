using Contract;
using GreenPipes;
using MassTransit;
using System;
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
            while (i <= 3000)
            {
                //System.Console.WriteLine($"Salve in file: {i}");
                bus.Publish<ISaveMessageCommand>(new
                {
                    id = i,
                    text = $"Hello World, {i}"
                });
                i += 1;
                Thread.Sleep(150);
                //System.Console.WriteLine("Waint 150....n");
            }

            bus.Stop();
        }

        static IBusControl ConfigureBus()
        {

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://rabbitmq3-management/"), h =>
                {
                    h.Username("NORUSDEV");
                    h.Password("teste1234");
                });

                sbc.ReceiveEndpoint(host, "ISaveConfirmedEvent", ep =>
                {
                    ep.PrefetchCount = 1;
                    //ep.UseMessageRetry(x => x.Interval(2, 100));
                    ep.Consumer<ConsumerConfimedEvent>();
                });
            });

            return bus;
        }
    }
}

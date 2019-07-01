using MassTransit;
using System;

namespace Mass_Transit
{
    public class MassTransit
    {
        public static void Main()
        {

            var busControl = ConfigureBus();
            busControl.Start();

            //string text = "Hello Mass+Transit";
            while(true)
            {
                Console.WriteLine("Enter your message ou quit to exit");
                string text = Console.ReadLine();

                if (text.Equals("exit"))
                {
                    break;
                }
                busControl.Publish<MessageText>(new
                {
                    Text = text
                });
                //busControl.CreateRequestClient<>
            }
            
            busControl.Stop();
        }
        static IBusControl ConfigureBus()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                //sbc.UseJsonSerializer();
                var host = sbc.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                                                // FILA QUE CONSUMER ESCUTA
                //sbc.ReceiveEndpoint(host, "teste", e =>
                //{
                //    e.Consumer<ConsumerMessage>();
                //});
            });
            return busControl;
        }
    }
}

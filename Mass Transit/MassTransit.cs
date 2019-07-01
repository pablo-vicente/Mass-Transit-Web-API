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

            string text2 = "Hello Mass+Transit";
            int i = 0;
            while(true)
            {
                Console.WriteLine("Enter your message ou quit to exit");
                string text = $"{text2} Number:{i}";
                Console.WriteLine(text);

                if (text.Equals("exit"))
                {
                    break;
                }
                busControl.Send<MessageText>(new
                {
                    MessageText = text
                });
                i += 1;
            }
            
            busControl.Stop();
        }

        static IBusControl ConfigureBus()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.UseJsonSerializer();
                var host = sbc.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                                                // FILA QUE CONSUMER ESCUTA
                sbc.ReceiveEndpoint(host, "testeWEBAPI", e =>
                {
                    //e.Consumer<ConsumerMessage>();
                });
            });
            return busControl;
        }
    }
}

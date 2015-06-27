using System;
using Microsoft.ServiceBus.Messaging;

namespace ReceiveFromQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBusRecieve();
        }

        private static void ServiceBusRecieve()
        {
            string qName = "76BusQueue";
            string connection =
                "Endpoint=sb://alewife.servicebus.windows.net/;SharedAccessKeyName=76BusReceiver;SharedAccessKey=/ot1r8HCUNnLScKABbOHharCzOBQqoNtvbxlfVKPyRQ=;TransportType=Amqp";

            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(connection);

            QueueClient queue = factory.CreateQueueClient(qName);

            var counter = 1;
            while (true)
            {
                BrokeredMessage bm = queue.Receive();

                if (bm != null)
                {
                    try
                    {
                        Console.WriteLine("MessageId {0}", bm.MessageId);
                        Console.WriteLine(counter);
                        Console.WriteLine(bm.GetBody<string>());
                        bm.Complete();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        bm.Abandon();
                    }
                }
                counter++;
            }
        }
    }
}

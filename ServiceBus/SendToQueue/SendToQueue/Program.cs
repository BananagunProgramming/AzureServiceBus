using System;
using Microsoft.ServiceBus.Messaging;

namespace SendToQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBusSend();
        }

        private static void ServiceBusSend()
        {
            string qName = "76BusQueue";
            string connection =
                "Endpoint=sb://alewife.servicebus.windows.net/;SharedAccessKeyName=76BusSender;SharedAccessKey=o23NObgIFcYaSALAJZazrf9wTP/BXrahH8erpeoO4iY=;TransportType=Amqp";

            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(connection);

            QueueClient queue = factory.CreateQueueClient(qName);

            for (int i = 0; i < 100; i++)
            {
                string message = "Thurman Thomas is old -" + DateTime.UtcNow.Ticks;
                BrokeredMessage bm = new BrokeredMessage(message);

                queue.Send(bm);

                Console.WriteLine(i);
            }
        }
    }
}

using System;
using System.Text;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace SendToQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceBusSend();
            //ServiceTopicSend();
            //SendToSpecificSubscription(1);
            SendToEventHub();
        }

        private static void SendToEventHub()
        {
            var ehName = "davnaeventhub";
            var connection = "Endpoint=sb://alewife.servicebus.windows.net/;SharedAccessKeyName=Sender;SharedAccessKey=wIMXK45Z/9PufbJlttoEU5ohz/AhNQz6VBP14ZfLIh0=;TransportType=Amqp";

            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(connection);

            EventHubClient client = factory.CreateEventHubClient(ehName);

            for (var i = 0; i < 10000; i++)
            {
                var message = i + " event hub message";

                EventData data = new EventData(Encoding.UTF8.GetBytes(message));

                client.Send(data);
   
                Console.WriteLine(message);
            }
        }

        private static void SendToSpecificSubscription(int subscription)
        {
            var topicName = "topicdemo";
            var connection = "Endpoint=sb://alewife.servicebus.windows.net/;SharedAccessKeyName=Sender;SharedAccessKey=sbAZpGVBq/LxeOeiIA8eilS2R392rujGv9Nxknd251M=";


            var ns = NamespaceManager.CreateFromConnectionString(connection);

            SqlFilter filter = new SqlFilter("Priority == 1");

            ns.CreateSubscription(topicName, "Subscription1", filter);

            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(connection);

            TopicClient topic = factory.CreateTopicClient(topicName);

            BrokeredMessage bm = new BrokeredMessage("this is a priority beetle!");
            bm.Properties["Priority"] = 1;

            topic.Send(bm);
            
        }

        private static void ServiceTopicSend()
        {
            var topicName = "topicdemo";
            var connection = "Endpoint=sb://alewife.servicebus.windows.net/;SharedAccessKeyName=Sender;SharedAccessKey=sbAZpGVBq/LxeOeiIA8eilS2R392rujGv9Nxknd251M=;TransportType=Amqp";

            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(connection);

            TopicClient topic = factory.CreateTopicClient(topicName);

            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
                topic.Send(new BrokeredMessage("topic message" + i));
            }
        }

        private static void ServiceBusSend()
        {
            var qName = "76BusQueue";
            var connection = "Endpoint=sb://alewife.servicebus.windows.net/;SharedAccessKeyName=76BusSender;SharedAccessKey=o23NObgIFcYaSALAJZazrf9wTP/BXrahH8erpeoO4iY=;TransportType=Amqp";

            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(connection);

            QueueClient queue = factory.CreateQueueClient(qName);

            for (var i = 0; i < 100; i++)
            {
                string message = "Thurman Thomas is old -" + DateTime.UtcNow.Ticks;
                BrokeredMessage bm = new BrokeredMessage(message);

                queue.Send(bm);

                Console.WriteLine(i);
            }
        }
    }
}

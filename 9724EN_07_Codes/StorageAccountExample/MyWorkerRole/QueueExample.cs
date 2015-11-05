using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWorkerRole
{
    public class QueueExample
    {
        private CloudQueueClient cloudQueueClient;
        public QueueExample()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
        }

        public void CreateQueue(string queueName)
        {
            CloudQueue cloudQueue = cloudQueueClient.GetQueueReference(queueName);
            cloudQueue.CreateIfNotExists();
        }

        public void Enqueue(string queueName, string message)
        {
            CloudQueue cloudQueue = cloudQueueClient.GetQueueReference(queueName);
            CloudQueueMessage cmessage = new CloudQueueMessage(message);

            cloudQueue.AddMessage(cmessage);
        }

        public void Dequeue(string queueName)
        {
            CloudQueue cloudQueue = cloudQueueClient.GetQueueReference(queueName);
            CloudQueueMessage cmessage = cloudQueue.PeekMessage(); //Gets message without removing it

            Trace.WriteLine(cmessage.AsString);

            cmessage = cloudQueue.GetMessage(); //Gets message and makes it invisible for 30 sec
            cloudQueue.DeleteMessage(cmessage); //deletes the message
        }
        public void UpdateMessage(string queueName, string message)
        {
            CloudQueue cloudQueue = cloudQueueClient.GetQueueReference(queueName);
            CloudQueueMessage cmessage = cloudQueue.GetMessage();
            cmessage.SetMessageContent(message);
            cloudQueue.UpdateMessage(cmessage, TimeSpan.FromSeconds(0d), MessageUpdateFields.Content | MessageUpdateFields.Visibility);
        }

    }
}

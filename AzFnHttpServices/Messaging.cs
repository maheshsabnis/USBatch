using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
 

namespace AzFnHttpServices
{
    /// <summary>
    /// The class used toWrite Data in Queue Storage
    /// </summary>
    public class Messaging
    {
        public async Task AddEntityToQueueAsync(string data)
        {
            // 1. Connect to the Storage Account using the ConnectionString
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=wmjuneusstorageaccount;AccountKey=upsaJ9r3AMJZT/S7IDgE097yB2CEse6uoUFs77tEjYjrqIqyq0fYjP7k8slv4sS8nuBs7y7mm20f+ASt69MzIA==;EndpointSuffix=core.windows.net");
            // 2. Create a Queue Storage Client
            CloudQueueClient cloudQueueClient = storageAccount.CreateCloudQueueClient();

            // 3. Get the Queue Reference and Create it if it is not exist
            CloudQueue cloudQueue = cloudQueueClient.GetQueueReference("entity-data-queue");
            await cloudQueue.CreateIfNotExistsAsync();

            // 4. Define a queue message

            CloudQueueMessage queueMessage = new CloudQueueMessage(data);

            // 5. Add Message to Queue
            await cloudQueue.AddMessageAsync(queueMessage);
        }
    }
}

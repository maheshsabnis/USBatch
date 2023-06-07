using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzFnQueueTrigger
{
    public class Function1
    {
        /// <summary>
        /// This function will be executed when the data is present into the queue
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <param name="log"></param>
        [FunctionName("Function1")]
        public void Run([QueueTrigger("entity-data-queue", Connection = "myqueueconection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}

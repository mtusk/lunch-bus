using Microsoft.WindowsAzure.Storage;

namespace LunchBus.Storage
{
    public class LunchBusContext
    {
        public Tables.Buses Buses { get; }

        public LunchBusContext(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var tableClient = storageAccount.CreateCloudTableClient();

            this.Buses = new Tables.Buses(tableClient);
        }
    }
}

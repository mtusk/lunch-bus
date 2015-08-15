using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace LunchBus.Storage.Entities
{
    public class User : TableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        
        public User(string partitionKey, Guid id)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = id.ToString();

            this.Id = id.ToString();
        }

        public User() { }
    }
}

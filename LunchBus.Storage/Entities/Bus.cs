using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace LunchBus.Storage.Entities
{
    public class Bus : TableEntity
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; }

        public DateTime DepartureDay { get; set; }
        public DateTime DepartureTime { get; set; }

        public string DestinationName { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationState { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationPostalCode { get; set; }

        public Bus(DateTimeOffset departureDay, string name)
        {
            this.PartitionKey = departureDay.UtcDateTime.Date.ToString("o").ToAzureKeyString();
            this.RowKey = name.ToAzureKeyString();

            this.DepartureDay = departureDay.UtcDateTime;
            this.Name = name;
        }

        public Bus() { }
    }
}

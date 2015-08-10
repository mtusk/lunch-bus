using System;

namespace LunchBus.Api.Models
{
    public class Bus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DepartureTime { get; set; }

        public string DestinationName { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationState { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationPostalCode { get; set; }
    }
}
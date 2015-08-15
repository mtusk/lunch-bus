using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchBus.Model
{
    public class Bus
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; }

        public DateTimeOffset DepartureTime { get; set; }

        public string DestinationName { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationState { get; set; }
        public string DestinationCountry { get; set; }
        public string DestinationPostalCode { get; set; }
    }
}

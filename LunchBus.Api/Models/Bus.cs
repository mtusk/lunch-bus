using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LunchBus.Api.Models
{
    public class Bus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DestinationAddress { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
using LunchBus.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LunchBus.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BusesController : ApiController
    {
        [HttpGet]
        public IEnumerable<Bus> Get()
        {
            var buses = Enumerable
                .Range(0, 5)
                .Select(i => new Bus
                {
                    Id = i,
                    DepartureTime = DateTime.Now.AddHours(i),
                    DestinationAddress = "123 Fake St. Someplace, AA 12345",
                    Name = "My First Bus"
                });

            return buses;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Bus bus)
        {
            // TODO: save the bus somewhere
            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}

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
            var buses = this.GenerateTestBuses();
            return buses;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Bus bus)
        {
            // TODO: save the bus somewhere
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        private IEnumerable<Bus> GenerateTestBuses()
        {
            var buses = Enumerable
                .Range(1, Faker.NumberFaker.Number(2, 25))
                .Select(i => new Bus
                {
                    Id = i,
                    Name = Faker.NameFaker.FirstName(),
                    DestinationName = Faker.CompanyFaker.Name(),
                    DestinationAddress = Faker.LocationFaker.Street(),
                    DestinationCity = Faker.LocationFaker.City(),
                    DestinationState = "MN",
                    DestinationCountry = Faker.LocationFaker.Country(),
                    DestinationPostalCode = Faker.LocationFaker.ZipCode(),
                    DepartureTime = Faker.DateTimeFaker.DateTime(DateTime.Now, DateTime.Now.AddDays(2)),
                });

            return buses;
        }
    }
}

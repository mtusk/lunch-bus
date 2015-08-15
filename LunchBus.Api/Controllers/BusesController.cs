using LunchBus.Model;
using LunchBus.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public IEnumerable<Bus> GetAsync()
        {
            var connectionStringSetting = ConfigurationManager.ConnectionStrings["StorageConnectionString"];

            if (connectionStringSetting == null)
            {
                throw new ConfigurationErrorsException("No connection string found");
            }

            var connectionString = connectionStringSetting.ConnectionString;
            var context = new LunchBusContext(connectionString);

            var upcomingBuses = context.Buses.GetUpcoming(TimeSpan.FromDays(2));

            return upcomingBuses;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Bus bus)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            var context = new LunchBusContext(connectionString);

            context.Buses.Create(bus);

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}

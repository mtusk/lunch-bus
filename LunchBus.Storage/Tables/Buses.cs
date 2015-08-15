using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;
using System;
using System.Collections.Generic;

namespace LunchBus.Storage.Tables
{
    public class Buses : TableBase<Entities.Bus>
    {
        #region TableBase
        public override string TableName
        {
            get { return "Buses"; }
        }

        public Buses(CloudTableClient tableClient) : base(tableClient) { }
        #endregion

        public Model.Bus Get(DateTimeOffset departureDay, string name)
        {
            var entity = base.Retrieve(departureDay.Date.ToString(), name);
            var bus = this.GetModel(entity);

            return bus;
        }

        public IEnumerable<Model.Bus> GetByDepartureDay(DateTimeOffset departureDay)
        {
            var entities = base.RetrieveAllInPartition(departureDay.Date.ToString());
            var buses = entities
                .Select(this.GetModel)
                .ToList();

            return buses;
        }

        public IEnumerable<Model.Bus> GetUpcoming(TimeSpan duration)
        {
            var maximumDateTime = DateTimeOffset.Now.Add(duration);
            var entities = new List<Entities.Bus>();

            var daysInTheFuture = Math.Ceiling(duration.TotalDays);
            for (int i = 0; i < daysInTheFuture; i++)
            {
                var day = DateTimeOffset.Now.Date.AddDays(i).Date;
                var busesThatDay = base.RetrieveAllInPartition(day.ToString());
                var busesThatDayWithinTheTimeWindow = busesThatDay.Where(bus => bus.DepartureTime < maximumDateTime);

                entities.AddRange(busesThatDayWithinTheTimeWindow);
            }

            var buses = entities
                .Select(this.GetModel)
                .ToList();

            return buses;
        }

        public void Create(Model.Bus bus)
        {
            var entity = this.GetEntity(bus);

            base.Insert(entity);
        }

        #region Helper Methods
        private Model.Bus GetModel(Entities.Bus entity)
        {
            Model.Bus model = null;

            if (entity != null)
            {
                model = new Model.Bus();
                model.DepartureTime = entity.DepartureTime;
                model.DestinationAddress = entity.DestinationAddress;
                model.DestinationCity = entity.DestinationCity;
                model.DestinationCountry = entity.DestinationCountry;
                model.DestinationName = entity.DestinationName;
                model.DestinationPostalCode = entity.DestinationPostalCode;
                model.DestinationState = entity.DestinationState;
                model.Name = entity.Name;
                model.OwnerId = entity.OwnerId;
            }

            return model;
        }

        private Entities.Bus GetEntity(Model.Bus model)
        {
            Entities.Bus entity = null;

            if (model != null)
            {
                entity = new Entities.Bus(
                    model.DepartureTime,
                    model.Name);
                entity.DepartureTime = model.DepartureTime.UtcDateTime;
                entity.DestinationAddress = model.DestinationAddress;
                entity.DestinationCity = model.DestinationCity;
                entity.DestinationCountry = model.DestinationCountry;
                entity.DestinationName = model.DestinationName;
                entity.DestinationPostalCode = model.DestinationPostalCode;
                entity.DestinationState = model.DestinationState;
                entity.OwnerId = model.OwnerId;
            }

            return entity;
        }
        #endregion
    }
}

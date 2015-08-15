using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace LunchBus.Storage.Tables
{
    public class Users : TableBase<Entities.User>
    {
        // Nothing better to partition on
        private const string partitionKey = "Users";

        #region TableBase
        public override string TableName
        {
            get { return "Users"; }
        }

        public Users(CloudTableClient tableClient) : base(tableClient) { }
        #endregion

        public Model.User Get(Guid id)
        {
            var entity = base.Retrieve(Users.partitionKey, id.ToString());
            var bus = this.GetModel(entity);

            return bus;
        }
        
        public IEnumerable<Model.User> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Create(Model.User user)
        {
            var entity = this.GetEntity(user);

            base.Insert(entity);
        }

        #region Helper Methods
        private Model.User GetModel(Entities.User entity)
        {
            Model.User model = null;

            if (entity != null)
            {
                model = new Model.User();
                model.Id = Guid.Parse(entity.Id);
                model.Name = entity.Name;
                model.PhotoUrl = entity.PhotoUrl;
            }

            return model;
        }

        private Entities.User GetEntity(Model.User model)
        {
            Entities.User entity = null;

            if (model != null)
            {
                entity = new Entities.User(
                    Users.partitionKey,
                    model.Id);
                entity.Name = model.Name;
                entity.PhotoUrl = model.PhotoUrl;
            }

            return entity;
        }
        #endregion
    }
}

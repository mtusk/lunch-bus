using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LunchBus.Storage.Tables
{
    public abstract class TableBase<TEntity> where TEntity : ITableEntity, new()
    {
        public CloudTable Table { get; }
        public abstract string TableName { get; }

        public TableBase(CloudTableClient tableClient)
        {
            this.Table = tableClient.GetTableReference(this.TableName);
            this.Table.CreateIfNotExists();
        }

        internal TEntity Retrieve(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<TEntity>(partitionKey, rowKey);
            var operationResult = this.Table.Execute(operation);

            TEntity entity;

            if (operationResult.Result == null)
            {
                entity = default(TEntity);
            }
            else
            {
                entity = (TEntity)operationResult.Result;
            }

            return entity;
        }

        internal IEnumerable<TEntity> RetrieveAllInPartition(string partitionKey)
        {
            var query = new TableQuery<TEntity>()
                .Where(TableQuery.GenerateFilterCondition(
                    "PartitionKey",
                    QueryComparisons.Equal,
                    partitionKey));
            var entities = this.Table.ExecuteQuery<TEntity>(query);

            return entities;
        }

        internal void Insert(ITableEntity entity)
        {
            var operation = TableOperation.Insert(entity);

            this.Table.Execute(operation);
        }
    }
}
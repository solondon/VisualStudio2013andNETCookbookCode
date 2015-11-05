using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWorkerRole
{

    public class TableExample
    {
        private CloudTableClient cloudTableClient;
        public TableExample()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
        }

        public void CreateTable(string tableName)
        {
            CloudTable table = cloudTableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
        }
        public void DeleteTable(string tableName)
        {
            CloudTable table = cloudTableClient.GetTableReference(tableName);
            table.DeleteIfExists();
        }

        public void ListTables(string tablePrefix)
        {
            var listTable = cloudTableClient.ListTables(tablePrefix);
            foreach (var table in listTable)
                Trace.WriteLine(table.Name);
        }
        public void AddEntryToTable(string tableName)
        {
            CloudTable table = cloudTableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            TableBatchOperation batch = new TableBatchOperation();
            batch.Add(TableOperation.Insert(new TableEntity { RowKey = "Abhishek", PartitionKey = "Kolkata" }));
            batch.Add(TableOperation.Insert(new TableEntity { RowKey = "Abhijit", PartitionKey = "Kolkata" }));
            table.ExecuteBatch(batch);
        }

        public void AddEntryToTable(string tableName, TableData customData)
        {
            CloudTable table = cloudTableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            var insertOperation = TableOperation.Insert(customData);
            table.Execute(insertOperation);
        }

        public void UpdateEntryToTable(string tableName, TableData customData)
        {
            CloudTable table = cloudTableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            var replaceOperation = TableOperation.Replace(customData);
            table.Execute(replaceOperation);
        }
        public void DeleteEntryToTable(string tableName, TableData customData)
        {
            CloudTable table = cloudTableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            var deleteOperation = TableOperation.Delete(customData);
            table.Execute(deleteOperation);
        }
        public void QueryTable(string tableName, string partitionKey, string rowKey)
        {
            CloudTable table = cloudTableClient.GetTableReference(tableName);
            TableQuery<TableData> tableQuery = new TableQuery<TableData>().Where(TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThan, rowKey)));

            foreach (TableData data in table.ExecuteQuery(tableQuery))
                Trace.WriteLine(data.ToString());
        }
        public void InsertData()
        {
            string tableName = "table1";
            this.CreateTable(tableName);
            this.AddEntryToTable(tableName);
            this.DeleteTable(tableName);

            TableData data = new TableData("Kolkata", "Sumit");
            data.Age = 20;
            this.AddEntryToTable(tableName, data);
            
            //Update data
            data.Age = 30;
            this.UpdateEntryToTable(tableName, data);

            this.ListTables(tableName);

            this.DeleteEntryToTable(tableName, data);

            this.DeleteTable(tableName);
        }

    }
}

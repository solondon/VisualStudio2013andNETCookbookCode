using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWorkerRole
{
    public class TableData : TableEntity
    {
        public TableData() { }
        public TableData(string partition, string row)
        {
            this.PartitionKey = partition;
            this.RowKey = row;
        }

        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("Partition: {0}, Row : {1}, Age : {2}", this.PartitionKey, this.RowKey, this.Age)
        }
    }
}

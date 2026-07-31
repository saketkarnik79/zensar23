using Azure;
using Azure.Data.Tables;

namespace DemoTableStorage.Models
{
    public class EmployeeEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = default!;
        public string RowKey { get; set; } = default!;

        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}

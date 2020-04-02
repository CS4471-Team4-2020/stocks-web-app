using System;

namespace StocksWebApp.Models
{
    public class StocksInfo
    {
     public string PartitionKey { get; set;}
     public string RowKey { get; set; }
     public DateTime Date { get; set; }
     public double Price { get; set; }
    }
}

using SalesWebMvcApp.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesWebMvcApp.Models
{
    public class SalesRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public SaleStatus Status { get; set; }
        public  Saller Seller { get; set; }

        public SalesRecord()
        {

        }

        public SalesRecord(DateTime date, double amount, SaleStatus status, Saller seller)
        {
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}

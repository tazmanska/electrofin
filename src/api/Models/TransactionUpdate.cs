using System;

namespace api.Models
{
    public class TransactionUpdate
    {
        public int CategoryId { get; set; }

        public int AccountId { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Amount { get; set; }

        public decimal Fee { get; set; }
    }
}
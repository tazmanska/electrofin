using System;

namespace api.Models
{
    public class TransferUpdate
    {
        public int FromAccountId { get; set; }

        public int ToAccountId { get; set; }

        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public decimal Fee { get; set; }
    }
}
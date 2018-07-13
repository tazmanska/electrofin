using System;

namespace api.Models
{
    public class Transfer
    {
        public int Id { get; set; }

        public int FromAccountId { get; set; }

        public int ToAccountId { get; set; }

        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public decimal Fee { get; set; }

        public string[] Tags { get; set; }
    }
}
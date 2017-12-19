using System;

namespace api.Dtos
{
    public class TransactionDto : BaseDto
    {
        public int AccountId { get; set; }

        public int CategoryId { get; set; }

        public DateTime DateTime { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public decimal Fee { get; set; }

        public decimal FinalCost => Amount + Fee;
    }
}
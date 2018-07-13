using System;

namespace api.Dtos
{
    public class AccountDto : BaseDto
    {
        public string Description { get; set; }

        public decimal Balance { get; set; }
    }
}
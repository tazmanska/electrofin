using System;

namespace api.Dtos
{
    public class AccountDto : BaseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Balance { get; set; }
    }
}
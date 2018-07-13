using System;

namespace api.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Balance { get; set; }

        public string[] Tags { get; set; }
    }
}
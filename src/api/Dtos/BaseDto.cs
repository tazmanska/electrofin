using System;

namespace api.Dtos
{
    public abstract class BaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string[] Tags { get; set; }

        public bool Removed { get; set; }
    }
}
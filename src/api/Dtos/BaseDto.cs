using System;

namespace api.Dtos
{
    public abstract class BaseDto
    {
        public int Id { get; set; }

        public bool Removed { get; set; }
    }
}
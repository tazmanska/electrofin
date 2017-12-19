using System;
using api.Enums;
using api.Dtos;

namespace api.Dtos
{
    public class CategoryDto : BaseDto
    {
        public int ParentCategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CategoryType Type { get; set; }

        public decimal Balance { get; set; }
    }
}
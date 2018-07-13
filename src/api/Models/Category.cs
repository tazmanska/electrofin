namespace api.Models
{
    public class Category
    {
        public int Id { get; set; }

        public int ParentCategoryId { get; set; }

        public bool Income { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string[] Tags { get; set; }
    }
}
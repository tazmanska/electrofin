
namespace api.Models
{
    public class CategoryUpdate
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string[] Tags { get; set; }
    }
}
using System;
using System.Linq;
using api.Dtos;
using api.Enums;
using api.Repositories;

namespace api.Services
{
    public class CategoryService : BaseService<CategoryDto>
    {
        public CategoryDto[] GetAllCategories(CategoryType categoryType)
        {
            return Repository.All()
                                      .Where(x => !x.Removed)
                                      .Where(x => x.Type == categoryType)
                                      .OrderBy(x => x.Name)
                                      .ToArray();
        }
    }
}
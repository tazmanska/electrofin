using System;
using System.Linq;
using api.Dtos;
using api.Enums;
using api.Extensions;
using api.Repositories;

namespace api.Services
{
    public class CategoryService : BaseService<CategoryDto>
    {
        public DataListDto<CategoryDto> GetAllCategories(CategoryType categoryType)
        {
            return Repository.All()
                                      .Where(x => x.Type == categoryType)
                                      .OrderBy(x => x.Name)
                                      .ToPagedData();
        }

        public override bool Remove(int id)
        {
            // remove transactions

            // remove cascade

            throw new NotImplementedException();
        }
    }
}
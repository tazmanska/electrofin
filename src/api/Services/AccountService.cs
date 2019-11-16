using System;
using System.Linq;
using api.Dtos;
using api.Extensions;
using api.Repositories;

namespace api.Services
{
    public class AccountService : BaseService<AccountDto>
    {
        public DataListDto<AccountDto> GetAll()
        {
            return Repository.All()
                                .OrderBy(x => x.Name)
                                .ToPagedData();
        }
    }
}
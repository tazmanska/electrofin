using System;
using System.Linq;
using api.Dtos;
using api.Repositories;

namespace api.Services
{
    public class AccountService : BaseService<AccountDto>
    {
        public AccountDto[] GetAll()
        {
            return Repository.All()
                                     .Where(x => !x.Removed)
                                     .OrderBy(x => x.Name)
                                     .ToArray();
        }
    }
}
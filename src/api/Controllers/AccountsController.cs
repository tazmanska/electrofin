using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Services;
using api.Models;
using api.Dtos;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : BaseController<AccountDto, AccountService>
    {
        [HttpGet]
        public IActionResult Get()
        {
            var result = Service.GetAll();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Account account)
        {
            var result = Service.Create(new AccountDto()
            {
                Name = account.Name,
                Description = account.Description,
                Tags = account.Tags,
                Balance = account.Balance
            });

            return Created("api/accounts/" + result.Id, result);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] AccountUpdate accountUpdate)
        {
            var account = Service.Get(id);

            if (account == null)
            {
                return NotFound();
            }

            account.Name = accountUpdate.Name;
            account.Description = accountUpdate.Description;
            account.Tags = accountUpdate.Tags;
            account.Balance = accountUpdate.Balance;

            Service.Update(account);

            return Ok(account);
        }
    }
}
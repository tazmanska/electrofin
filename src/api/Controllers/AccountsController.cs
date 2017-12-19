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
    public class AccountsController : Controller
    {
        private AccountService _accountService = new AccountService();

        [HttpGet]
        public IActionResult Get()
        {
            var result = _accountService.GetAll();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var result = _accountService.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Account account)
        {
            var result = _accountService.Create(new AccountDto()
            {
                Name = account.Name,
                Description = account.Description
            });

            return Created("api/accounts/" + result.Id, result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var account = _accountService.Get(id);

            if (account == null)
            {
                return NotFound();
            }

            _accountService.Remove(id);

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] AccountUpdate accountUpdate)
        {
            var account = _accountService.Get(id);

            if (account == null)
            {
                return NotFound();
            }

            account.Name = accountUpdate.Name;
            account.Description = accountUpdate.Description;

            _accountService.Update(account);

            return Ok(account);
        }
    }
}
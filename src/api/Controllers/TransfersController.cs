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
    public class TransfersController : Controller
    {
        private TransferService _transferService = new TransferService();

        [HttpGet]
        public IActionResult Get()
        {
            var result = _transferService.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var result = _transferService.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Transfer transfer)
        {
            var result = _transferService.Create(new TransferDto()
            {
                ToAccountId = transfer.ToAccountId,
                FromAccountId = transfer.FromAccountId,
                DateTime = transfer.DateTime,
                Amount = transfer.Amount,
                Fee = transfer.Fee,
                Description = transfer.Description
            });

            return Created("api/transfers/" + result.Id, result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var account = _transferService.Get(id);

            if (account == null)
            {
                return NotFound();
            }

            _transferService.Remove(id);

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] TransferUpdate transferUpdate)
        {
            var transfer = _transferService.Get(id);

            if (transfer == null)
            {
                return NotFound();
            }

            transfer.ToAccountId = transferUpdate.ToAccountId;
            transfer.FromAccountId = transferUpdate.FromAccountId;
            transfer.DateTime = transferUpdate.DateTime;
            transfer.Amount = transferUpdate.Amount;
            transfer.Fee = transferUpdate.Fee;
            transfer.Description = transferUpdate.Description;

            _transferService.Update(transfer);

            return Ok(transfer);
        }
    }
}
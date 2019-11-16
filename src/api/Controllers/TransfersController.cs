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
    public class TransfersController : BaseController<TransferDto, TransferService>
    {
        [HttpGet]
        public IActionResult Get()
        {
            var result = Service.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Transfer transfer)
        {
            var result = Service.Create(new TransferDto()
            {
                ToAccountId = transfer.ToAccountId,
                FromAccountId = transfer.FromAccountId,
                DateTime = transfer.DateTime,
                Amount = transfer.Amount,
                Fee = transfer.Fee,
                Description = transfer.Description,
                Tags = transfer.Tags
            });

            return Created("api/transfers/" + result.Id, result);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] TransferUpdate transferUpdate)
        {
            var transfer = Service.Get(id);

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
            transfer.Tags = transferUpdate.Tags;

            Service.Update(transfer);

            return Ok(transfer);
        }
    }
}
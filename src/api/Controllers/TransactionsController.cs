using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Services;
using api.Models;
using api.Dtos;
using api.Enums;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : BaseController<TransactionDto, TransactionService>
    {
        [HttpGet]
        [Route("")]
        public IActionResult Get(int? categoryId = null, DateTime? from = null, DateTime? to = null, bool descending = true, int? page = 1, int? pageSize = 50, string[] tags = null)
        {
            var result = Service.GetTransactionsByCategoryId(categoryId, from, to, descending, page ?? 1, pageSize ?? 50);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            var result = Service.Create(new TransactionDto()
            {
                CategoryId = transaction.CategoryId,
                Description = transaction.Description,
                AccountId = transaction.AccountId,
                DateTime = transaction.DateTime,
                Amount = transaction.Amount,
                Fee = transaction.Fee,
                Tags = transaction.Tags
            });

            return Created("", result);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] TransactionUpdate transactionUpdate)
        {
            var transaction = Service.Get(id);

            if (transaction == null)
            {
                return NotFound();
            }

            transaction.AccountId = transactionUpdate.AccountId;
            transaction.CategoryId = transactionUpdate.CategoryId;
            transaction.Description = transactionUpdate.Description;
            transaction.DateTime = transactionUpdate.DateTime;
            transaction.Amount = transactionUpdate.Amount;
            transaction.Fee = transactionUpdate.Fee;
            transaction.Tags = transactionUpdate.Tags;

            Service.Update(transaction);

            return Ok(transaction);
        }
    }
}
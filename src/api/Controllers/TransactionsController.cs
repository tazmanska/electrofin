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
    public class TransactionsController : Controller
    {
        private TransactionService _transactionService = new TransactionService();

        [HttpGet]
        [Route("{{categoryId}}")]
        public IActionResult Get(int categoryId)
        {
            var result = _transactionService.GetTransactionsByCategoryId(categoryId);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            var result = _transactionService.Create(new TransactionDto()
            {
                CategoryId = transaction.CategoryId,
                Description = transaction.Description,
                AccountId = transaction.AccountId,
                DateTime = transaction.DateTime,
                Amount = transaction.Amount,
                Fee = transaction.Fee
            });

            return Created("", result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var transaction = _transactionService.Get(id);

            if (transaction == null)
            {
                return NotFound();
            }

            _transactionService.Remove(id);

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] TransactionUpdate transactionUpdate)
        {
            var transaction = _transactionService.Get(id);

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

            _transactionService.Update(transaction);

            return Ok(transaction);
        }
    }
}
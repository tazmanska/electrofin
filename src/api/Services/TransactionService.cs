using System;
using System.Linq;
using api.Dtos;
using api.Extensions;
using api.Repositories;

namespace api.Services
{
    public class TransactionService : BaseService<TransactionDto>
    {
        private BaseRepository<AccountDto> _accountRepository = new BaseRepository<AccountDto>();
        private BaseRepository<CategoryDto> _categoryRepository = new BaseRepository<CategoryDto>();

        public DataListDto<TransactionDto> GetTransactionsByCategoryId(int? categoryId, DateTime? from, DateTime? to, bool descending = true, int page = 1, int pageSize = 10)
        {
            if (from != null)
            {
                from = from.Value.Date;
            }            

            if (to != null)
            {
                to = from.Value.AddDays(1).Date;
            }

            return Repository.All(x => (categoryId == null || x.CategoryId == categoryId) && (from == null || x.DateTime >= from) && (to == null || x.DateTime < to))
                                         .OrderBy(x => x.DateTime)
                                         .ToPagedData(page, pageSize);
        }

        public override TransactionDto Create(TransactionDto transactionDto)
        {            
            var transaction = Repository.Add(transactionDto);
            HandleTransaction(transaction, false);
            return transaction;
        }

        public override bool Remove(int id)
        {
            var transaction = Repository.GetById(id);
            HandleTransaction(transaction, true);
            return Repository.Remove(id);
        }

        public override bool Update(TransactionDto transaction)
        {
            var oldTransaction = Repository.GetById(transaction.Id);
            HandleTransaction(oldTransaction, true);
            HandleTransaction(transaction, false);
            return Repository.Update(transaction);
        }

        private void HandleTransaction(TransactionDto transaction, bool withdraw)
        {
            var factor = withdraw ? -1m : 1m;
            var account = _accountRepository.GetById(transaction.AccountId);
            var category = _categoryRepository.GetById(transaction.CategoryId);
            UpdateAccountAndCategory(account, category, factor * -transaction.FinalCost);
            _accountRepository.Update(account);
            _categoryRepository.Update(category);
        }

        private void UpdateAccountAndCategory(AccountDto account, CategoryDto category, decimal amount)
        {
            if (category.Type == Enums.CategoryType.Income)
            {
                account.Balance += amount;
                category.Balance += amount;
            }
            else
            {
                account.Balance -= amount;
                category.Balance -= amount;
            }
        }
    }
}
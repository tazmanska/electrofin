using System;
using System.Linq;
using api.Dtos;
using api.Repositories;

namespace api.Services
{
    public class TransactionService : BaseService<TransactionDto>
    {
        private BaseRepository<AccountDto> _accountRepository = new BaseRepository<AccountDto>();
        private BaseRepository<CategoryDto> _categoryRepository = new BaseRepository<CategoryDto>();

        public TransactionDto[] GetTransactionsByCategoryId(int? categoryId, DateTime? from, DateTime? to, bool descending = true, int page = 0, int pageSize = 10)
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
                                         .Skip(page * pageSize)
                                         .Take(pageSize)
                                         .ToArray();
        }

        public override TransactionDto Create(TransactionDto transactionDto)
        {            
            var transaction = Repository.Add(transactionDto);
            var account = _accountRepository.GetById(transaction.AccountId);
            account.Balance -= transaction.FinalCost;
            _accountRepository.Update(account);
            var category = _categoryRepository.GetById(transaction.CategoryId);
            category.Balance += transaction.FinalCost;
            _categoryRepository.Update(category);
            return transaction;
        }

        public override bool Remove(int id)
        {
            var transaction = Repository.GetById(id);
            var account = _accountRepository.GetById(transaction.AccountId);
            account.Balance += transaction.FinalCost;
            _accountRepository.Update(account);
            var category = _categoryRepository.GetById(transaction.CategoryId);
            category.Balance -= transaction.FinalCost;
            return Repository.Remove(id);
        }

        public override bool Update(TransactionDto transaction)
        {
            var oldTransaction = Repository.GetById(transaction.Id);

            if (oldTransaction.AccountId != transaction.AccountId)
            {
                var oldAccount = _accountRepository.GetById(oldTransaction.AccountId);
                var newAccount = _accountRepository.GetById(transaction.Id);
                oldAccount.Balance += oldTransaction.FinalCost;
                _accountRepository.Update(oldAccount);
                newAccount.Balance -= transaction.FinalCost;
                _accountRepository.Update(newAccount);
            }

            if (oldTransaction.CategoryId != transaction.CategoryId)
            {
                var oldCategory = _categoryRepository.GetById(oldTransaction.CategoryId);
                var newCategory = _categoryRepository.GetById(transaction.CategoryId);
                oldCategory.Balance -= oldTransaction.FinalCost;
                _categoryRepository.Update(oldCategory);
                newCategory.Balance += transaction.FinalCost;
                _categoryRepository.Update(newCategory);
            }

            return Repository.Update(transaction);
        }
    }
}
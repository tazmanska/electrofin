using System.Linq;
using api.Dtos;
using api.Repositories;

namespace api.Services
{
    public class TransferService : BaseService<TransferDto>
    {
        private BaseRepository<AccountDto> _accountRepository = new BaseRepository<AccountDto>();

        public TransferDto[] GetAll()
        {
            return Repository.All()
                             .Where(x => !x.Removed)
                             .OrderBy(x => x.DateTime)
                             .ToArray();
        }

        public override TransferDto Create(TransferDto model)
        {
            var accountFrom = _accountRepository.GetById(model.FromAccountId);
            var accountTo = _accountRepository.GetById(model.ToAccountId);
            accountFrom.Balance -= model.Amount + model.Fee;
            _accountRepository.Update(accountFrom);
            accountTo.Balance += model.Amount;
            _accountRepository.Update(accountTo);
            return Repository.Add(model);
        }

        public override bool Remove(int id)
        {
            var transfer = Repository.GetById(id);
            var accountFrom = _accountRepository.GetById(transfer.FromAccountId);
            var accountTo = _accountRepository.GetById(transfer.ToAccountId);
            accountFrom.Balance += transfer.Amount + transfer.Fee;
            _accountRepository.Update(accountFrom);
            accountTo.Balance -= transfer.Amount;
            _accountRepository.Update(accountTo);
            return Repository.Remove(id);
        }

        public override bool Update(TransferDto transfer)
        {
            var oldTransfer = Repository.GetById(transfer.Id);

            if (oldTransfer.FromAccountId != transfer.FromAccountId)
            {
                var oldAccountFrom = _accountRepository.GetById(oldTransfer.FromAccountId);
                var newAccountFrom = _accountRepository.GetById(transfer.ToAccountId);
                oldAccountFrom.Balance += transfer.Amount + transfer.Fee;
                _accountRepository.Update(oldAccountFrom);
                newAccountFrom.Balance -= transfer.Amount + transfer.Fee;
                _accountRepository.Update(newAccountFrom);
            }

            if (oldTransfer.ToAccountId != transfer.ToAccountId)
            {
                var oldAccountTo = _accountRepository.GetById(oldTransfer.FromAccountId);
                var newAccountTo = _accountRepository.GetById(transfer.ToAccountId);
                oldAccountTo.Balance -= transfer.Amount;
                _accountRepository.Update(oldAccountTo);
                newAccountTo.Balance += transfer.Amount;
                _accountRepository.Update(newAccountTo);
            }

            return Repository.Update(transfer);
        }
    }
}
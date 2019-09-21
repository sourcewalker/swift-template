using Core.Infrastructure.Interfaces.DAL;
using $safeprojectname$.Interfaces;
using Core.Shared.DTO;
using System;

namespace $safeprojectname$.Domain
{
    public class FailedTransactionService : IFailedTransactionService
    {
        private readonly IFailedTransactionRepository _failedRepository;

        public FailedTransactionService(IFailedTransactionRepository failedRepository)
        {
            _failedRepository = failedRepository;
        }

        public bool Create(FailedTransactionDto transaction)
        {
            return _failedRepository.Add(transaction);
        }

        public bool Delete(Guid id)
        {
            return _failedRepository.Delete(id);
        }
    }
}

using Core.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace $safeprojectname$
{
    public interface IFailedTransactionService
    {
        bool Create(FailedTransactionDto transaction);

        bool Delete(Guid id);
    }
}

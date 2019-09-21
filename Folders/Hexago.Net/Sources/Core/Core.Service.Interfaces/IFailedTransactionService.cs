using Core.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public interface IFailedTransactionService
    {
        bool Create(FailedTransactionDto transaction);

        bool Delete(Guid id);
    }
}

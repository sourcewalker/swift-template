using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Ecommerce.Entities
{
    public enum PaymentStatus
    {
        Captured = 0,
        Rejected = 1,
        Secured = 2,
        PendingAuthorization = 3
    }
}

using Swift.Umbraco.Business.Manager.Interfaces;
using Swift.Umbraco.Infrastructure.$safeprojectname$.Petapoco;
using Swift.Umbraco.Models.Domain;

namespace Swift.Umbraco.Infrastructure.$safeprojectname$.Entities
{
    public class FailedTransactionManager : GenericManager<FailedTransaction>, IFailedTransactionManager
    {
        public FailedTransactionManager()
        {

        }
    }
}

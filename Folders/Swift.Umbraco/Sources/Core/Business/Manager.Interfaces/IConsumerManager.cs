using Swift.Umbraco.Models.Enum;
using System;

namespace Swift.Umbraco.$safeprojectname$.Manager.Interfaces
{
    public interface IConsumerManager
    {
        Guid GetOrCreateConsumerId(string consumerId, Countries country, string EmailHash);

        bool IsAlreadyExistingConsumerId(Countries country, string emailHash, out Guid consumerCrmId);
    }
}

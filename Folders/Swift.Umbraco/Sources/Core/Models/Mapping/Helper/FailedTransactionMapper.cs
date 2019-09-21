using Swift.Umbraco.$safeprojectname$.Domain;
using Swift.Umbraco.$safeprojectname$.DTO;
using System.Collections.Generic;

namespace Swift.Umbraco.$safeprojectname$.Mapping.Helper
{
    public static class FailedTransactionMapper
    {
        public static FailedTransactionDto toDto(this FailedTransaction failedTransaction)
            => AutoMapper.Mapper.Map<FailedTransaction, FailedTransactionDto>(failedTransaction);

        public static FailedTransaction toEntity(this FailedTransactionDto failedTransactionDto)
            => AutoMapper.Mapper.Map<FailedTransactionDto, FailedTransaction>(failedTransactionDto);

        public static IEnumerable<FailedTransaction> toEntities(this IEnumerable<FailedTransactionDto> failedTransactionDtos)
            => AutoMapper.Mapper.Map<IEnumerable<FailedTransactionDto>, IEnumerable<FailedTransaction>>(failedTransactionDtos);

        public static IEnumerable<FailedTransactionDto> toDtos(this IEnumerable<FailedTransaction> failedTransactions)
            => AutoMapper.Mapper.Map<IEnumerable<FailedTransaction>, IEnumerable<FailedTransactionDto>>(failedTransactions);
    }
}

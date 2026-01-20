using TdMarzPay.Models.Responses;

namespace TdMarzPay.Interfaces;

public interface ITransactions
{
    Task<TdMarzTransaction> GetTransaction(Guid refferenceId);
}
using TdMarzPay.Models.Responses;

namespace TdMarzPay.Interfaces;

public interface ITransactions
{
    Task<MarzTransaction> GetTransaction(Guid refferenceId);
}
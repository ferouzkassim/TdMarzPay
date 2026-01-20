using TdMarzPay.Models.Commands;
using TdMarzPay.Models.Responses;

namespace TdMarzPay.Interfaces;
/// <summary>
///     Gets the CollectMoney interface for handling money collection(Get Funds from a  user) operations.
/// </summary>
public interface ICollectMoney
{
    /// <summary>
    ///     Initiates a money collection transaction from a user
    /// returns GenericResponse with SuccessResponse data on successful initiation
    /// </summary>
    Task<GenericResponse<SuccessResponse>?> InitiateTransaction(CollectMoney collectMoney);
    Task MoneyServices();
    Task MoneyService(Guid guid);
}
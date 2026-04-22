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
    Task<GenericResponse<MarzRepsonse>> InitiateTransaction(MarzCollectMoneyRequest collectMoney);
    Task MoneyServices();
    Task<bool> TransactionDetails(Guid guid);

    /// <summary>
    /// 1. Handle the webhook from MarzPay
    /// 2. Validate the webhook signature
    /// 3. Handle the webhook
    /// </summary>
    /// <param name="callBack"></param>
    /// <returns></returns>
    GenericResponse<MarzCallBackResponse> HandleWebhook(MarzPayCallBack callBack);
}
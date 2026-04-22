using TdMarzPay.Models.Commands;

namespace TdMarzPay.Interfaces;

public interface IUtilities
{
    Task PayBill(TdMarzUtility utilities);
    Task VerifyBill(TdMarzUtility utilities);
    void GetWaterUtilityGeo(TdMarzUtility utilities);
    void GetCableTvBouquets(TdMarzUtility utilities);
    void GetTransaction(string referenceId);
    Task<string> InitiateTransaction(TdMarzUtility utilities);
}
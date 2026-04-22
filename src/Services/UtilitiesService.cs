using TdMarzPay.Interfaces;
using TdMarzPay.Models.Commands;
using TdMarzPay.Models.Shared;
using TdMarzPay.Shared;

namespace TdMarzPay.Services;

public class UtilitiesService(BaseConfiguration configuration):IUtilities
{
    private readonly HttpClient _httpClient = configuration.CreateInstance();
    public Task PayBill(TdMarzUtility utilities)
    {
     
        throw new NotImplementedException();
    }

    public Task VerifyBill(TdMarzUtility utilities)
    {
        throw new NotImplementedException();
    }

    public void GetWaterUtilityGeo(TdMarzUtility utilities)
    {
        throw new NotImplementedException();
    }

    public void GetCableTvBouquets(TdMarzUtility utilities)
    {
        throw new NotImplementedException();
    }

    public void GetTransaction(string referenceId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> InitiateTransaction(TdMarzUtility utilities)
    {
        var form =   utilities.ToFormUrlEncodedContent(UseCases.PayTvBill);
        var response = await _httpClient.PostAsync("bill-payment", form);
        Console.WriteLine(response);
        return await response.Content.ReadAsStringAsync();
    }
}
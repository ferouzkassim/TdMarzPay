using System.Net.Http.Json;
using TdMarzPay.Interfaces;
using TdMarzPay.Models.Commands;
using TdMarzPay.Models.Responses;
using TdMarzPay.Shared;

namespace TdMarzPay.Services;

public class CollectMoneyService(BaseConfiguration  config):ICollectMoney
{
    private readonly BaseConfiguration _baseConfiguration = config;

    private HttpClient GetClient()
    {
        return config.CreateInstance();
    }
    /// <summary>
    /// Initiates a Collect Money transaction
    /// its upto to you to listen to the webhook or poll for the transaction status using the reference provided
    /// or await this object to see if its a success or failure
    /// </summary>
    /// <param name="collectMoney"></param>
    /// <returns></returns>
    public async Task<GenericResponse<SuccessResponse>?> InitiateTransaction(CollectMoney collectMoney)
    {
        var content = CollectMoney.CollectMoneyForm(collectMoney);
        
     var res =   await GetClient().
                                    PostAsync(
                                        "collect-money",
                                        content);
     return await res.Content.ReadFromJsonAsync<GenericResponse<SuccessResponse>>();
    }
    

    public async Task MoneyServices()
    {
       var response = await GetClient().GetAsync("collect-money/services");
       Console.WriteLine(response);
    }

    public async Task MoneyService(Guid guid)
    {
        var response = await GetClient().GetAsync($"collect-money/{guid}");
    }
}
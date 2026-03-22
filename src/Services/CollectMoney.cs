using System.Net.Http.Json;
using System.Text.Json;
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
        return _baseConfiguration.CreateInstance();
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
       var strig = await response.Content.ReadAsStringAsync();
       Console.WriteLine(response);
    }

    public async Task<bool> TransactionDetails(Guid guid)
    {
        var response = await GetClient().GetAsync($"collect-money/{guid}");
        var res= await response.Content.ReadAsStringAsync();
        var root = JsonDocument.Parse(res);
        var data = root.RootElement.GetProperty("status");
        return string.Compare(data.GetString(), "success", StringComparison.OrdinalIgnoreCase) == 0;
    }
}
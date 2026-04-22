using System.Text.Json;
using TdMarzPay.Interfaces;
using TdMarzPay.Models.Commands;
using TdMarzPay.Models.Responses;
using TdMarzPay.Shared;
using TdMarzPay.Shared.MapObjects;

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
    public async Task<GenericResponse<MarzRepsonse>> InitiateTransaction(MarzCollectMoneyRequest collectMoney)
    {
        var content = MarzCollectMoneyRequest.CollectMoneyForm(collectMoney);
        
     var res =   await GetClient().
                                    PostAsync(
                                        "collect-money",
                                        content);
     var apiResponse =await res.Content.ReadAsStringAsync();
     if (res is { IsSuccessStatusCode: false, ReasonPhrase: not null })
     {
         return GenericResponse<MarzRepsonse>.Failed(res.StatusCode.ToString(), apiResponse);
     }
     return JsonSerializer.Deserialize<GenericResponse<MarzRepsonse>>(apiResponse) ?? 
            GenericResponse<MarzRepsonse>.Failed("500", "Internal Server Error");
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
        Console.WriteLine(res);
        return string.Compare(data.GetString(), "success", StringComparison.OrdinalIgnoreCase) == 0;
    }

  
    public GenericResponse<MarzCallBackResponse> HandleWebhook(MarzPayCallBack callBack)
    {
        return callBack.Transaction == null ? GenericResponse<MarzCallBackResponse>.Failed("failed")
            : GenericResponse<MarzCallBackResponse>.Success(CallBackMap.ToCallBackResponse(callBack));
    }
    
}
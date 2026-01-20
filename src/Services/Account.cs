using System.Net.Http.Json;
using TdMarzPay.Interfaces;
using TdMarzPay.Models.Responses;
using TdMarzPay.Shared;

namespace TdMarzPay.Services;

public class Account(BaseConfiguration configuration):IAccount
{
    private readonly BaseConfiguration _configuration = configuration;
    private HttpClient GetClient()
    {
        return _configuration.CreateInstance();
    }
    public async Task GetAccountDetails()
    {
     //var res =  await GetClient().GetFromJsonAsync<MarzPayResponse<AccountResponse>>("account");
     
    }

    public void UpdateAccountDetails()
    {
        throw new NotImplementedException();
    }
}
using System.Net.Http.Json;
using System.Text.Json;
using TdMarzPay.Interfaces;
using TdMarzPay.Models.Responses;
using TdMarzPay.Shared;

namespace TdMarzPay.Services;

public class Transactions(BaseConfiguration configuration):ITransactions
{
    private readonly BaseConfiguration _configuration = configuration;
    private HttpClient GetClient()
    {
        return _configuration.CreateInstance();
    }
    public async Task<TdMarzTransaction> GetTransaction(Guid transactionId)
    {
       var res = await GetClient().GetStringAsync($"/transactions/{transactionId}");

       return JsonSerializer.Deserialize<TdMarzTransaction>(res) ?? throw new Exception("Transaction not found");
    }
}
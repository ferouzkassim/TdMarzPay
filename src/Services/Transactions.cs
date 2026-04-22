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
    public async Task<MarzTransaction> GetTransaction(Guid transactionId)
    {
       var res = await GetClient().GetStringAsync($"/transactions/{transactionId}");

       return JsonSerializer.Deserialize<MarzTransaction>(res) ?? throw new Exception("Transaction not found");
    }
}
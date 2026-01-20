using Microsoft.Extensions.DependencyInjection;
using Moq;
using TdMarzPay;
using TdMarzPay.Interfaces;
using TdMarzPay.Models;
using TdMarzPay.Models.Responses;

namespace TestProject1;

public class Transactions
{
    private readonly IServiceProvider _serviceProvider;

    public Transactions()
    {
        _serviceProvider = Adddi().BuildServiceProvider();
    }


    private static ServiceCollection Adddi()
    {
        var diServce = new ServiceCollection();
     
        diServce.Configure<MarzPayConfiguration>(config =>
        {
            config.ApiKey ="your api key";
            config.ApiSecret = "your api secret";
        });
        diServce.AddMarzPay();
        return diServce;
    }

    [Fact]
    public async Task Get_Balance_List()
    {
        var mockITransaction = new Mock<ITransactions>();
        mockITransaction.Setup(x => x.GetTransaction(Guid.NewGuid())).ReturnsAsync(new TdMarzTransaction());
        
        var balance = _serviceProvider.GetService<ITransactions>();
        var respo = await balance?.GetTransaction(Guid.NewGuid());
        Assert.NotNull(respo);
        Assert.Equal("success", respo.Business);
    } 
}
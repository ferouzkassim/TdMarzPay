using Microsoft.Extensions.DependencyInjection;
using TdMarzPay.Models;

namespace TdMarzPay.Tests;

public class Balances
{
   
    private readonly IServiceProvider _serviceProvider = Adddi().BuildServiceProvider();


    private static ServiceCollection Adddi()
    {
        var diServce = new ServiceCollection();
     
        diServce.Configure<MarzPayConfiguration>(config =>
        {
            config.ApiKey ="your api key";
            config.ApiSecret = "your api secret";
            config.TimeOut = 1000;
        });
        diServce.AddMarzPay();
        return diServce;
    }
    [Fact]
    public async Task GetBalance_With_clear_Credentials()
    {
        
       /* var balancePay = _serviceProvider.GetService<IBalance>();
        var respo = await balancePay?.GetBalance();
        Assert.NotNull(respo);
        Assert.Equal("success", respo.Status);
        
        Assert.True(respo.Status == "success");
        */
    }
}
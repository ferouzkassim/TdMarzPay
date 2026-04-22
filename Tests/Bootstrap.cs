using Microsoft.Extensions.DependencyInjection;
using TdMarzPay;
using TdMarzPay.Models;

namespace TestProject1;

public class Bootstrap
{
    public static ServiceCollection AddPipeline()
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
}
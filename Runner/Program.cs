using Microsoft.Extensions.DependencyInjection;
using TdMarzPay;
using TdMarzPay.Interfaces;
using TdMarzPay.Models;
using TdMarzPay.Models.Commands;

namespace Runner
{
    public class Runner
    {
        private static IServiceCollection GetServices()
        {
            var services = new ServiceCollection();
            services.Configure<MarzPayConfiguration>(mprs =>
            {
                mprs.ApiKey = Environment.GetEnvironmentVariable("MARZ_API_KEY")!;
                mprs.ApiSecret = Environment.GetEnvironmentVariable("MARZ_API_SECRET")!;
                mprs.SetTimeOut(1000);//incase  you want to change the default timeout
                mprs.SetBaseUrl("https://wallet.wearemarz.com/api/v1");

            });
            return services.AddMarzPay();
        }

     
        public static async Task Main()
        {
           var provide = GetServices().BuildServiceProvider();
           var coollectmoney = CollectMoney.Collect(5000)
                                           .WithPhoneNumber("+256703988005")
                                           .Verify();
           var utility = TdMarzUtility.InitiateUtility("12345678901")
               .WithCableTv();
           
            var balanceService = provide.GetRequiredService<IMarzPay>();
            var req = await balanceService.Utilities.InitiateTransaction(utility);
         var res =  await balanceService.CollectMoney.InitiateTransaction(coollectmoney);
         var trx = provide.GetRequiredService<ITransactions>();
         await balanceService.CollectMoney.MoneyServices();
         await balanceService.CollectMoney.TransactionDetails(Guid.Parse("ceb9b642-86fd-4022-8727-2b446556b484"));
         Console.WriteLine(res?.ErrorCode);
         Console.WriteLine(res?.Status);
         Console.WriteLine(res?.Message);
        
        }
    }
   
}
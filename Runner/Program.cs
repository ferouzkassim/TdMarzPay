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
                mprs.ApiKey = "";
                mprs.ApiSecret = "";
               
                mprs.SetTimeOut(1000);//incase  you want to change the default timeout
                mprs.SetBaseUrl("https://wallet.wearemarz.com/api/v1");

            });
            return services.AddMarzPay();
        }

     
        public static async Task Main()
        {
           var provide = GetServices().BuildServiceProvider();
           var coollectmoney = new CollectMoney()
           {
              Amount = 500,
              PhoneNumber = "+256703988005",
              Country = "UG",
              Reference = Guid.NewGuid().ToString(), 
              
           };
            var balanceService = provide.GetRequiredService<IMarzPay>();
         var res =  await balanceService.CollectMoney.InitiateTransaction(coollectmoney);
         var trx = provide.GetRequiredService<ITransactions>();
        
         
         Console.WriteLine(res?.ErrorCode);
         Console.WriteLine(res?.Status);
         Console.WriteLine(res?.Message);
         var trns = await trx.GetTransaction(Guid.Parse(res?.Data?.Transaction.Reference));
         Console.WriteLine(trns?.Business);
         Console.WriteLine("done");
        }
    }
   
}
using Microsoft.Extensions.DependencyInjection;
using TdMarzPay.Interfaces;
using TdMarzPay.Services;
using TdMarzPay.Shared;

namespace TdMarzPay
{
   public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMarzPay(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient();
            serviceCollection.AddScoped<IMarzPay,MarzPayService>();
            serviceCollection.AddScoped<BaseConfiguration>();
            serviceCollection.AddScoped<IAccount,Account>();
            serviceCollection.AddScoped<ICollectMoney,CollectMoneyService>();
            serviceCollection.AddScoped<ITransactions,Transactions>();
            serviceCollection.AddScoped<ICollectMoney,CollectMoneyService>();
            return serviceCollection;
        }

       
    }
}

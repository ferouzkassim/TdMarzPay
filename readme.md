A .net C# sdk for the MARZPAY payment platform https://wallet.wearemarz.com/ 
##
first things first
```
dotnet add package TdMarzPay 
```

All we need is to configure your MarzPayConfiguration
```
 builder.Services.Configure<MarzPayConfiguration>(mprs =>
{
    mprs.ApiKey = Environment.GetEnvironmentVariable("MARZ_API_KEY")!;
    mprs.ApiSecret = Environment.GetEnvironmentVariable("MARZ_API_SECRET")!;
    mprs.SetTimeOut(1000);//incase  you want to change the default timeout
    mprs.SetBaseUrl("https://wallet.wearemarz.com/api/v1");
});

```

Plase do note hardcode your keys look out for a key-vault manager how ever 
the MarzPayConfiguration is :
```
public class MarzPayConfiguration
    {
        public string ApiKey { get; set; }  
        public string ApiSecret { get; set; }
        public int TimeOut { get; set; }
        public string? BaseUrl { get; set; }
    }
/*
in future if baseUrl changes you can manually add it , you can add the timeout but even if not given we still have a default, but apikey and secret are neede at all costs 

*/
```
then 
```
services.AddMarzPay();
```
---
### To collect Funds ?
 ```
  var collecmoneyObject = new CollectMoney()
           {
              Amount = 500,
              PhoneNumber = "+2567123456789",
              Country = "UG",
              Reference = Guid.NewGuid().ToString(), /// id refeenrec 
              CallbackUrl = new Uri("mywebseite.com")
              /*this can be nullable if web hook is set at dashboard */
              
           };
 ```
Optionally
```
 var res = await IMarzPay.CollectMoney.InitiateTransaction(collecmoneyObject);
  res is a a generic responce dto from the marzpay collection
  if you want to verify the integrity of the object you can use the fluent Api and use
  verify method
 var collectmoney = MarzCollectMoney.Collect(5000)
                                          .WithPhoneNumber("+25611111154")
                                          .WithDescription("Collecting money")
                                          .WithReference(Guid.Parse("ceb9b642-86fd-4022-8727-2b446556b484"))
                                          .WithCallbackUrl(new Uri("https://webhook.site/ab6a2792-8b9a-46c7-850b-a9ebfb5310bb"))
                                          .Verify();
 ```
---
```
var res = await IMarzPay.CollectMoney.InitiateTransaction(collecmoneyObject);
// res is a a generic responce dto from the marzpay collection
the response dto is :
public class MarzCallback
{
 
    public MarzTransaction Transaction { get; set; }
  
    public Collection Collection { get; set; }

    public string EventType { get; set; }

    public string Mode { get; set; }
    public bool IsSuccess 
    
}
//you will need to check isSuccess its already generated  for you which 
 
```
We Included a free way to hadle the webhook below:
```plantuml
app.MapPost("/Callback",  ([FromBody] MarzPayCallBack callback,[FromServices]IMarzPay mp) =>
{
   var res =  mp.CollectMoney.HandleWebhook(callback);
   
   return res.Data!=null ? Results.Ok(res.Data) : Results.BadRequest(res.ErrorCode);
})

```
> res is a a generic responce dto from the marzpay collection
>> which is denoted by a record type
 ```plantuml
public record MarzCallBackResponse(
                string Refference,
                bool IsSuccess,
                string Message,
                MarzPayEvents EventType,
                string ProviderReference);
```
---
note: this is not the official package but i thought i could share since i used and it worked for more information you can 
find support [MarzPay](https://wallet.wearemarz.com/landing-support) 
---
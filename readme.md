A .net C# sdk for the MARZPAY payment platform https://wallet.wearemarz.com/ 
##
first things first
```
dotnet add package TdMarzPay 
```

All we need is to configure your MarzPayConfiguration
```
 services.Configure<MarzPayConfiguration>(mprs =>
            {
                mprs.ApiKey = "your_api_key";
                mprs.ApiSecret = "your_api_secret";
                mprs.BaseUrl = "";
                mprs.TimeOut = 1000;

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
 var res = await IMarzPay.CollectMoney.InitiateTransaction(collecmoneyObject);
 // res is a a geenric responce dto from the marzpay collection
 ```
---
note: this is not the official package but i thought i could share since i used and it worked for more information you can 
find support https://wallet.wearemarz.com/landing-support 
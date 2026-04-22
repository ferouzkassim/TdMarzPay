using System.Text.Json;
using Microsoft.Extensions.Options;
using Moq;
using TdMarzPay.Interfaces;
using TdMarzPay.Models;
using TdMarzPay.Models.Responses;
using TdMarzPay.Services;
using TdMarzPay.Shared;

namespace TestProject1;

public class WebhookTest
{


    [Theory]
    [InlineData("""
                {
                    "collection": {
                        "mode": "airteluganda",
                        "amount": {
                            "raw": 51500,
                            "currency": "UGX",
                            "formatted": "51,500.00"
                        },
                        "provider": "airtel",
                        "phone_number": "+2xxxxxx",
                        "provider_transaction_id": "NA"
                    },
                    "event_type": "collection.completed",
                    "transaction": {
                        "uuid": "0xxxxxxa61799b8",
                        "amount": {
                            "raw": "51500",
                            "currency": "UGX",
                            "formatted": "51,500.00"
                        },
                        "status": "completed",
                        "provider": "airtel",
                        "reference": "xxxxxdb54b",
                        "created_at": "2026-04-21T16:24:19.000000Z",
                        "updated_at": "2026-04-21T16:24:29.000000Z",
                        "description": "Sxxxx0 - Airtel Payment Requested - Auto-completed after 9 seconds",
                        "phone_number": "+xxxx4"
                    }
                }
                """, true)]
    [InlineData("""
                {
                    "collection": {
                        "mode": "airteluganda",
                        "amount": {
                            "raw": 103000,
                            "currency": "UGX",
                            "formatted": "103,000.00"
                        },
                        "provider": "airtel",
                        "phone_number": "+xxxxx",
                        "provider_transaction_id": "NA"
                    },
                    "event_type": "collection.failed",
                    "transaction": {
                        "uuid": "xxxx",
                        "amount": {
                            "raw": "103000",
                            "currency": "UGX",
                            "formatted": "103,000.00"
                        },
                        "status": "failed",
                        "provider": "airtel",
                        "reference": "xxxxx",
                        "created_at": "2026-04-21T11:45:44.000000Z",
                        "updated_at": "2026-04-21T11:49:04.000000Z",
                        "description": "dsfdg - Airtel Payment Requested - Auto-failed: No response after 3 minutes",
                        "phone_number": "xxxxxxx"
                    }
                }
                """,false)]
    [InlineData("""
                {"event_type":"collection.failed","transaction":{"uuid":"34972751-2292-4e7d-85c6-c0c0ac489aff","reference":"e09c3ac4-334c-4d09-a806-d0dde4d33d2a","status":"failed","amount":{"formatted":"10,300.00","raw":"10300","currency":"UGX"},"provider":"airtel","phone_number":"+yyyyyyy","description":"Collection - Airtel Payment Requested - Auto-failed: No response after 3 minutes","created_at":"2026-04-22T21:31:40.000000Z","updated_at":"2026-04-22T21:35:12.000000Z"},"collection":{"provider":"airtel","phone_number":"+yyyyyy","amount":{"formatted":"10,300.00","raw":10300,"currency":"UGX"},"mode":"airteluganda","provider_transaction_id":"NA"}}
                """,false)]
    public async Task HandleCallBack(string json, bool expected)
    {
        var httpmock = new Mock<IHttpClientFactory>();
        var ioptionsmonitor = new Mock<IOptionsMonitor<MarzPayConfiguration>>();
       var config = new BaseConfiguration(httpmock.Object,ioptionsmonitor.Object);
       var resMock = new CollectMoneyService(config);
       var js = JsonSerializer.Deserialize<MarzPayCallBack>(json);
       if (js is not null)
       {
        var result =   resMock.HandleWebhook(js);
        if (expected)
        {
            Assert.IsType<GenericResponse<MarzCallBackResponse>>(result);
            Assert.Equal("success",result.Status);
            Assert.Equal("Sxxxx0 - Airtel Payment Requested - Auto-completed after 9 seconds",result.Data.Message);
            Assert.IsType<MarzCallBackResponse>(result.Data);
            Assert.True(result.Data.IsSuccess);
            Assert.Equal("xxxxxdb54b",result.Data.Refference);
            Assert.Equal(MarzPayEvents.Completed,result.Data.EventType);
        }
        else
        {
            if (result.Data != null) Assert.NotEqual(MarzPayEvents.Completed, result.Data.EventType);
            Assert.IsType<GenericResponse<MarzCallBackResponse>>(result);
           
        }
       }
      
    
    }

}
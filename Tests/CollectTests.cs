using System.Net;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using TdMarzPay.Interfaces;
using TdMarzPay.Models;
using TdMarzPay.Models.Commands;
using TdMarzPay.Models.Responses;
using TdMarzPay.Services;
using TdMarzPay.Shared;

namespace TestProject1;

public class CollectTests
{
   
    [Fact]
    public async Task CollectWhenBodyIsFine()
    {
        var httpmessage = new Mock<HttpMessageHandler>();
          httpmessage.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(), 
            ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("""
                                            {
                                              "status": "success",
                                              "message": "Collection initiated successfully.",
                                              "data": {
                                                "transaction": {
                                                  "uuid": "4e7fb3fa-c13a-4b05-8acd-cf60ff68cb94",
                                                  "reference": "COL1703123456789abc",
                                                  "status": "processing",
                                                  "provider_reference": null
                                                },
                                                "collection": {
                                                  "amount": {
                                                    "formatted": "1,000.00",
                                                    "raw": 1000,
                                                    "currency": "UGX"
                                                  },
                                                  "provider": "mtn",
                                                  "phone_number": "+256781230949",
                                                  "mode": "live"
                                                },
                                                "timeline": {
                                                  "initiated_at": "2024-01-20 14:30:00",
                                                  "estimated_settlement": "2024-01-20 14:35:00"
                                                }
                                              }
                                            }
                                            """)
            });
        var client = new HttpClient(httpmessage.Object);
        var baseconfig = new Mock<BaseConfiguration>();
        baseconfig.Setup(x => x.CreateInstance()).Returns(client);
        var marzpay = new Mock<IOptionsMonitor<MarzPayConfiguration>>();
        marzpay.Setup(x => x.CurrentValue).Returns(new MarzPayConfiguration());
        
       // baseconfig.Setup(x => x.CreateInstance(client,marzpay.Object)).Returns(new HttpClient(httpmessage.Object));

        var collectservice = new CollectMoneyService(baseconfig.Object);
        var marzcollect = MarzCollectMoneyRequest.Collect(1000)
          .WithPhoneNumber("256781230949")
          .WithDescription("Collecting money")
          .WithReference(Guid.NewGuid())
          .WithCallbackUrl(new Uri("https://webhook.site/ab6a2792-8b9a-46c7-850b-a9ebfb5310bb"));
        var response = await collectservice.InitiateTransaction(marzcollect);
        Assert.NotNull(response);
        Assert.IsType<GenericResponse<MarzRepsonse>>(response);
        Assert.IsType<MarzRepsonse>(response.Data);
        Assert.Equal("1000",response.Data.Transaction.Amount?.Raw);
        Assert.NotNull(response.Data.Transaction.MarzReference);
        Assert.Equal("success",response.Status);
        Assert.Equal("Collection initiated successfully.",response.Message);
        Assert.Equal("processing",response.Data.Transaction.Status);
        Assert.Equal("COL1703123456789abc",response.Data.Transaction.MarzReference);
    }

    [Fact]
    public async Task HandleCallBack()
    {
      var httpmessage = new Mock<HttpMessageHandler>();
      httpmessage.Protected().Setup<Task<HttpResponseMessage>>("sendAsync"
        , ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()
      ).ReturnsAsync(new HttpResponseMessage
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent("""
                                    { 
                                      "event_type":"collection.failed",
                                    "transaction":
                                      {
                                        "uuid":"13c7d9f4-729d-4fad-906d-33d91bebdd48",
                                        "reference":"3a47552c-974b-481b-8e21-d68bca988fc2",
                                        "status":"failed",
                                    "amount":
                                      {
                                        "formatted":"41,200.00",
                                        "raw":"41200",
                                        "currency":"UGX"
                                        },
                                        "provider":"mtn","phone_number":"+256771230533",
                                        "description":"Collection - MTN Payment Requested - Auto-failed: Still pending with provider after 5 minutes",
                                        "created_at":"2026-04-01T12:08:30.000000Z",
                                          "updated_at":"2026-04-01T12:14:11.000000Z"},
                                    "collection":
                                      {"provider":"mtn","phone_number":"+256772460533",
                                        "amount":
                                          {"formatted":"41,200.00","raw":41200,"currency":"UGX"},
                                      "mode":"mtnuganda","provider_transaction_id":"39635738285"}}
                                    """)
      });
      var client = new Mock<HttpClient>(httpmessage.Object);
      var marzpay = new Mock<IMarzPay>();
      
    }
    
}
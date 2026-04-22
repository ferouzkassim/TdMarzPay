using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TdMarzPay;
using TdMarzPay.Interfaces;
using TdMarzPay.Models;
using TdMarzPay.Models.Commands;
using TdMarzPay.Models.Responses;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MarzPayConfiguration>(mprs =>
{
    mprs.ApiKey = Environment.GetEnvironmentVariable("MARZ_API_KEY")!;
    mprs.ApiSecret = Environment.GetEnvironmentVariable("MARZ_API_SECRET")!;
    mprs.SetTimeOut(1000);//incase  you want to change the default timeout
    mprs.SetBaseUrl("https://wallet.wearemarz.com/api/v1");
});
builder.Services.AddCors();
builder.Services.AddMarzPay();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapPost("/CollectMoney", async ([FromBody]MarzCollectMoneyRequest collect,[FromServices] IMarzPay mp) =>
    {
        var results = await mp.CollectMoney.InitiateTransaction(collect.WithCallbackUrl(new Uri("https://webhook.site/51691c02-3c48-40a1-99cb-ca5190c15bae")));
        return results?.Data!=null ? Results.Ok(results.Data) : Results.BadRequest(results?.ErrorCode);
    })
    .WithOpenApi();
app.MapGet("/CheckReference",async ([FromQuery]Guid id,[FromServices]IMarzPay mp) =>
Results.Ok(await mp.CollectMoney.TransactionDetails(id))).WithOpenApi();
app.MapPost("/Callback",  ([FromBody] MarzPayCallBack callback,[FromServices]IMarzPay mp) =>
{
   var res =  mp.CollectMoney.HandleWebhook(callback);
   return res.Data!=null ? Results.Ok(res.Data) : Results.BadRequest(res.ErrorCode);
}).WithOpenApi().RequireCors(e =>
{
    e.AllowAnyOrigin();
    e.AllowAnyMethod();
    e.AllowAnyHeader();
    
});
app.MapGet("/checkTransaction",async 
    ([FromQuery] Guid txid, [FromServices] IMarzPay mp) =>
        Results.Ok(await mp.CollectMoney.TransactionDetails(txid)));
app.Run();

using Microsoft.AspNetCore.Mvc;
using TdMarzPay;
using TdMarzPay.Interfaces;
using TdMarzPay.Models;
using TdMarzPay.Models.Commands;
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
builder.Services.AddMarzPay();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapPost("/CheckTransaction", async ([FromBody]CollectMoney collect,[FromServices] IMarzPay mp) =>
    {
        var results = await mp.CollectMoney.InitiateTransaction(collect);
        return results?.Data!=null ? Results.Ok(results.Data) : Results.BadRequest(results?.Message);
    })
    .WithOpenApi();
app.MapGet("/CollectMoney",async ([FromQuery]Guid id,[FromServices]IMarzPay mp) =>
Results.Ok(await mp.CollectMoney.TransactionDetails(id))).WithOpenApi();
app.Run();

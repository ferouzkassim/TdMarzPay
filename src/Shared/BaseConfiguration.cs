using Microsoft.Extensions.Options;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TdMarzPay.Models;

namespace TdMarzPay.Shared
{
    public class BaseConfiguration(IHttpClientFactory httpClientFactory,IOptionsMonitor<MarzPayConfiguration> configuration)
    {
        private readonly IHttpClientFactory _httpClient = httpClientFactory;
        private readonly IOptionsMonitor<MarzPayConfiguration> _configuration = configuration;
        public HttpClient CreateInstance() {
          
          var marzClient =   _httpClient.CreateClient();
          marzClient.BaseAddress = new Uri($"{_configuration.CurrentValue.BaseUrl}/");
          marzClient.DefaultRequestHeaders.Add("Authorization",$"Basic {GetEncodedKey()}");
          marzClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue( "application/json"));
         
            return marzClient;


        }
        private string GetEncodedKey() {
            var config = _configuration.CurrentValue;
            var key = $"{config.ApiKey}:{config.ApiSecret}";
            var encodedKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
           return encodedKey;


        }
    }
}

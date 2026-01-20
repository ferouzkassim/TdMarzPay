using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TdMarzPay.Models
{
    /// <summary>
    /// MarzPay Configuration Settings Model Got from the wearemarzpay dashboard
    /// namely ApiKey, ApiSecret, TimeOut and BaseUrl
    /// </summary>
    /// 
    /// 
    public class MarzPayConfiguration
    {
        public string ApiKey { get; set; }  
        public string ApiSecret { get; set; }
        public int TimeOut { get; set; }
        public Uri? BaseUrl { get; private set; }
        public MarzPayConfiguration(){}
        public MarzPayConfiguration( string apikey, string apiSecret)
        {
            ApiKey = apikey;
            ApiSecret = apiSecret;
            TimeOut = 6000;
            BaseUrl =new Uri("https://wallet.wearemarz.com/api/v1");
        }
        public void SetTimeOut(int timeOut)
        {
            if(timeOut <= 0)
            {
                throw new ArgumentException("TimeOut must be greater than 0");
            }
            TimeOut = timeOut;
        }
        public void SetBaseUrl(string baseUrl)
        {
            BaseUrl = new Uri(baseUrl);
        }
    }
   
}

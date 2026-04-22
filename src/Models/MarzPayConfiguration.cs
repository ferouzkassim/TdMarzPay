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
        /// <summary>
        /// @default 0.03 ||3% of the orignal transaction amount
        /// </summary>
        public decimal? ChargeFee { get; set; }
        public MarzPayConfiguration(){}
        public MarzPayConfiguration( string apikey, string apiSecret)
        {
            ApiKey = apikey;
            ApiSecret = apiSecret;
            TimeOut = 6000;
            ChargeFee = 0.03M;
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

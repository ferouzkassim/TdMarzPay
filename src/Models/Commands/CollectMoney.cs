using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace TdMarzPay.Models.Commands;

public class MarzCollectMoneyRequest:BaseCollect
{
        
        [JsonPropertyName("country")]
        public string Country { get; private set; } = "UG";
        /// <summary>
        /// Reference is the id that you will need to use to monitor the transaction i advice to use a unique reference for each transaction
        /// </summary>
        [JsonPropertyName("reference")]
        [Required(ErrorMessage = "Reference is required")]
        public required Guid Reference { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get;private set; }
        /// <summary>
        /// if given this wil override the webhook url in the dashboard if not given the dashboard webhook url will be used
        /// </summary>

        [JsonPropertyName("callback_url")]
        public Uri? CallbackUrl { get;private set; }
        
        public MarzCollectMoneyRequest(){}
        

        /// <summary>
        /// starting point of the fluent syntax, creates a CollectMoney object
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static MarzCollectMoneyRequest Collect(decimal amount)
        {
               
                return new MarzCollectMoneyRequest()
                {
                        Amount =  amount,
                        Reference = Guid.NewGuid(),
                        PhoneNumber = "000000",
                        Country = "UG",
                        Description = "Collect Money"
                        
                };
        }
        /// <summary>
        /// String representation of the phone number to be collected from
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public MarzCollectMoneyRequest WithPhoneNumber(string phoneNumber)
        {       
                PhoneNumber = phoneNumber;
                return this;
        }
/// <summary>
/// 1. Set the reference to a unique value if you have it otherwise will use a random guid
/// </summary>
/// <param name="reference"></param>
/// <returns></returns>
        public MarzCollectMoneyRequest WithReference(Guid reference)
        {
                Reference = reference;
                return this;
        }
        public MarzCollectMoneyRequest WithDescription(string description)
        {
                Description = description;
                return this;
        }
        public MarzCollectMoneyRequest WithCallbackUrl(Uri callbackUrl)
        {
                CallbackUrl = callbackUrl;
                return this;
        }
        /// <summary>
        /// Verifies the CollectMoney object
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public MarzCollectMoneyRequest Verify()
        {
                if(Amount is < 500 or > 10000000) throw new ArgumentException("Out Of Range Amount");
                if(string.IsNullOrWhiteSpace(PhoneNumber)) throw new ArgumentException("Phone number is required");
                if(!Guid.TryParse(Reference.ToString(), out var resu)) throw new ArgumentException("Reference is required or malformated");
                return this;
        }
        /// <summary>
        /// Creates FormUrlEncodedContent from CollectMoney object to be sent as request body
        /// </summary>
        /// <param name="collectMoney"></param>
        /// <returns></returns>
        public static  FormUrlEncodedContent CollectMoneyForm(MarzCollectMoneyRequest collectMoney)
        {
                if(collectMoney.PhoneNumber.StartsWith("0"))
                { 
                        collectMoney.PhoneNumber = string.Concat("+256", collectMoney.PhoneNumber.AsSpan(1));
                }
                var requestForm = new Dictionary<string, string>
                {
                        { "amount", collectMoney.Amount.ToString(CultureInfo.CurrentCulture) },
                        { "phone_number", collectMoney.PhoneNumber },
                        { "country", collectMoney.Country },
                        { "reference", collectMoney.Reference.ToString() },
                        { "description", collectMoney.Description ?? string.Empty },
                        { "callback_url", collectMoney.CallbackUrl?.ToString() ?? string.Empty }

                };
            return new FormUrlEncodedContent(requestForm);
        }
        
}


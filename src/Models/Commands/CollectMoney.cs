using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using TdMarzPay.Models.JsonConvertors;

namespace TdMarzPay.Models.Commands;

public class CollectMoney
{
    
        /// <summary>
        ///    Amount to be collected from the user
        /// </summary>
        [JsonPropertyName("amount")]
        [JsonConverter(typeof(ConvertToMoney))]
        public required decimal Amount { get; set; }
        /// <summary>
        /// 
        /// Phone number of the user from whom money is to be collected airtel or mtn format
        /// </summary>
        [JsonPropertyName("phone_number")]
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public required string PhoneNumber { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; } = "UG";
        /// <summary>
        /// Reference is the id that you will need to use to monitor the transaction i advice to use a unique reference for each transaction
        /// </summary>
        [JsonPropertyName("reference")]
        [Required(ErrorMessage = "Reference is required")]
        public required string Reference { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
        /// <summary>
        /// if given this wil override the webhook url in the dashboard if not given the dashboard webhook url will be used
        /// </summary>

        [JsonPropertyName("callback_url")]
        public Uri? CallbackUrl { get; set; }
        
        public CollectMoney(){}
       
        public CollectMoney (decimal amount , string phoneNumber, string reference)
        {
                Amount = amount;
                PhoneNumber = phoneNumber;
                Reference = reference;
        }
        /// <summary>
        /// Creates FormUrlEncodedContent from CollectMoney object to be sent as request body
        /// </summary>
        /// <param name="collectMoney"></param>
        /// <returns></returns>
        public static  FormUrlEncodedContent CollectMoneyForm(CollectMoney collectMoney)
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
                        { "reference", collectMoney.Reference },
                        { "description", collectMoney.Description ?? string.Empty },
                        { "callback_url", collectMoney.CallbackUrl?.ToString() ?? string.Empty }

                };
            return new FormUrlEncodedContent(requestForm);
        }
        
}


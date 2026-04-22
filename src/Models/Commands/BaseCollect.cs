using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TdMarzPay.Models.JsonConvertors;

namespace TdMarzPay.Models.Commands;

public abstract class BaseCollect
{
    /// <summary>
    ///    Amount to be collected from the user
    /// </summary>
    [JsonPropertyName("amount")]
    [JsonConverter(typeof(ConvertFromMoney))]
    [Required(ErrorMessage = "Amount is required")]
    [Range(500, 10000000, ErrorMessage = "Amount must be between 500 and 10000000")]
    public required decimal Amount { get; set; }
    /// <summary>
    /// 
    /// Phone number of the user from whom money is to be collected airtel or mtn format
    /// </summary>
    [JsonPropertyName("phone_number")]
    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    [RegularExpression(@"^\+?[0-9]{10,12}$", ErrorMessage = "Invalid phone number format")]
    public required string PhoneNumber { get; set; }
} 
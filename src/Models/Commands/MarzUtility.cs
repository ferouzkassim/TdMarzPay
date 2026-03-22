using System.ComponentModel.DataAnnotations;
using System.Globalization;
using TdMarzPay.Models.Shared;

namespace TdMarzPay.Models.Commands;

public class TdMarzUtility : BaseCollect
{
    private Utilities Utility { get; set; }
    private string MeterNumber { get; set; }
    private string GeoArea { get; set; }
    private string? BouquetCode { get; set; }
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address")]
    private string CustomerEmail { get; set; }
    private Guid Reference { get; set; }
    private TdMarzUtility()
    {
        
    }

    public static TdMarzUtility InitiateUtility(string meterNumber,Guid? referenceId=null)
    {
        return new TdMarzUtility
        { 
            Reference = referenceId ?? Guid.NewGuid(),
            Amount = 0,
            PhoneNumber = string.Empty,
            MeterNumber = meterNumber,
            GeoArea = string.Empty
        };
    }

    public TdMarzUtility WithElectricityUtility()
    {
        Utility = Utilities.Light;
        return this;
    }

    public TdMarzUtility WithNwscUtility(string geoArea)
    {
        GeoArea = geoArea;
        Utility = Utilities.Nwsc;
        return this;
    }

    public TdMarzUtility WithCableTv()
    {
        Utility = Utilities.Dstv;
        return this;
    }
    public FormUrlEncodedContent ToFormUrlEncodedContent(UseCases useCase)
    {
        var baseDict = new Dictionary<string, string>
        {
            { "meter_number", MeterNumber },
            { "amount", Amount.ToString(CultureInfo.InvariantCulture) },
            { "utility_code", Utility.ToString().ToUpper(CultureInfo.InvariantCulture) },
            { "reference", Reference.ToString() },
          
        };
        switch (useCase)
        {
            case UseCases.PayTvBill:
                if(!string.IsNullOrEmpty(BouquetCode))baseDict.Add("bouquet_code", BouquetCode);
                break;
            case UseCases.CollectMoney:
                break;
            case UseCases.BillVerification:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(useCase), useCase, null);
        }
        if(Amount>0) baseDict.Add("amount", Amount.ToString(CultureInfo.InvariantCulture));
        if(!string.IsNullOrWhiteSpace(PhoneNumber)) baseDict.Add("phone_number", PhoneNumber);
        if(!string.IsNullOrWhiteSpace(CustomerEmail)) baseDict.Add("email", CustomerEmail);
        if(!string.IsNullOrWhiteSpace(GeoArea)) baseDict.Add("area", GeoArea);
        return new FormUrlEncodedContent(baseDict);
    }
}
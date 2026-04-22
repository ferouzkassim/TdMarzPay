namespace TdMarzPay.Models.Responses;

public record MarzCallBackResponse(
    string Refference,
    bool IsSuccess,
    string Message,
    MarzPayEvents EventType,
    string ProviderReference);
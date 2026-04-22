using TdMarzPay.Models;
using TdMarzPay.Models.Responses;

namespace TdMarzPay.Shared.MapObjects;

public static class CallBackMap
{
    public static Func<MarzPayCallBack, MarzCallBackResponse> ToCallBackResponse =
        response =>
        {
            if (response.Transaction == null)
                throw new Exception("Transaction not found");

            var isSucces = response.Transaction.Status == "completed";
            return new MarzCallBackResponse(
                Refference: response.Transaction.MarzReference ?? "reference",
                IsSuccess: isSucces,
                Message: response.Transaction.Description ?? "description",
                EventType:isSucces?MarzPayEvents.Completed:Enum.Parse<MarzPayEvents>(response.EventType?.Split(".").Last() ?? "failed",ignoreCase:true),
                ProviderReference:
                response.Transaction.ProviderTransactionReference
                ?? response.PhoneNumber
                ?? "providerTransactionId"
            );
        };
}
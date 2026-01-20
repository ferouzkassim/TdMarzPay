namespace TdMarzPay.Interfaces;

public interface IMarzPay
{
    /// <summary>
    ///     Gets the CollectMoney interface for handling money collection(Get Funds from a  user) operations.
    /// </summary>
    ICollectMoney CollectMoney { get; }
    IAccount Account { get; }
    ITransactions Transactions { get; }
}
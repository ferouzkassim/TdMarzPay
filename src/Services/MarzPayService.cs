using TdMarzPay.Interfaces;

namespace TdMarzPay.Services;

public class MarzPayService(ICollectMoney collectMoney,IAccount account,ITransactions txns,IUtilities utilities):IMarzPay
{
    public ICollectMoney CollectMoney { get; } = collectMoney;

    public IAccount Account { get; } = account;

    public ITransactions Transactions { get; } = txns;
    public IUtilities Utilities { get; } = utilities;
}
using TdMarzPay.Interfaces;

namespace TdMarzPay.Services;

public class MarzPayService(ICollectMoney collectMoney,IAccount account,ITransactions txns):IMarzPay
{
    private readonly ICollectMoney _collectMoney= collectMoney;
    private readonly IAccount _account = account;
    private readonly ITransactions _transactions = txns;
    public ICollectMoney CollectMoney =>_collectMoney;
    public IAccount Account =>_account;
    public ITransactions Transactions =>_transactions;
}
namespace TdMarzPay.Interfaces;

public interface IAccount
{
    Task GetAccountDetails();
    void UpdateAccountDetails();
}
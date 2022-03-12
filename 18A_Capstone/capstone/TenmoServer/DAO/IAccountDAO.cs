using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDao
    {
        Account GetAccountByUserId(int userId);
        Transfer UpdateAccountBalances(Transfer transfer);
        List<Account> GetAccounts();
    }
}

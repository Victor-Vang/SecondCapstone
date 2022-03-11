using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDao
    {
        Account GetAccountByUserId(int userId);
        Account GetAccountByAccountId(int accountId);
        Account UpdateAccount(Account account);
    }
}

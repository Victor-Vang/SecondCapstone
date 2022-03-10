using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDao
    {
        Account GetAccount(int userId);

        Account UpdateAccount(int userId, decimal moneySent);
    }
}

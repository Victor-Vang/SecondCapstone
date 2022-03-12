using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        Transfer AddTransfer(Transfer transfer);
        List<Transfer> GetTransfers(int accountId);
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TenmoServer.Models;
using TenmoServer.Security;
using TenmoServer.Security.Models;

namespace TenmoServer.DAO
{
    public class TransferDao : ITransferDao
    {
        private readonly string connectionString;

        private string sqlAddTransfer = "INSERT INTO transfer (transfer_type_id, transfer_status_id, account_from, account_to, amount) VALUES (2, 2, @account_from, @account_to, @amount);";

        private string sqlGetTransfers = "SELECT * FROM transfer WHERE account_to = @accountId OR account_from = @accountId ";
        public TransferDao(string connectionString)
        {
            this.connectionString = connectionString;
        }
       
        public Transfer AddTransfer(Transfer transfer)
        {
            //bool result = false;
            Transfer addedTransfer = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlAddTransfer, conn);
          

                cmd.Parameters.AddWithValue("@account_from", transfer.AccountFrom);
                cmd.Parameters.AddWithValue("@account_to", transfer.AccountTo);
                cmd.Parameters.AddWithValue("@amount", transfer.Amount);

                int count = cmd.ExecuteNonQuery();

                if (count > 0)
                {
                    addedTransfer = transfer;
                }
                return addedTransfer;
            }
        }

        public List<Transfer> GetTransfers(int accountId)
        {
            List<Transfer> transfers = new List<Transfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetTransfers, conn);
                    cmd.Parameters.AddWithValue("@accountId", accountId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Transfer transfer = TransferReader(reader);
                        transfers.Add(transfer);
                    }
                }
            }
            catch (Exception ex)
            {
                transfers = new List<Transfer>();
            }
            return transfers;
        }

        private Transfer TransferReader(SqlDataReader reader)
        {
            Transfer transfer = new Transfer();
            transfer.TransferId = Convert.ToInt32(reader["transfer_id"]);
            transfer.TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]);
            transfer.TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]);
            transfer.AccountFrom = Convert.ToInt32(reader["account_from"]);
            transfer.AccountTo = Convert.ToInt32(reader["account_to"]);
            transfer.Amount = Convert.ToDecimal(reader["amount"]);
            return transfer;
        }
    }
}

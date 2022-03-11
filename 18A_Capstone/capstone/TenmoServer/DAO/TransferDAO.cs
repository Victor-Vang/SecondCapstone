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
    }
}

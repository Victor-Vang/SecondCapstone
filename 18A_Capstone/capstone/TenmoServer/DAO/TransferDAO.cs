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
       
        public bool AddTransfer(Transfer transfer)
        {
            bool result = false;

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
                    result = true;
                }
                return result;
            }
        }



        //private Account GetAccountFromReader(SqlDataReader reader)
        //{
        //    Account account = new Account()
        //    {
        //        AccountId = Convert.ToInt32(reader["account_id"]),
        //        UserId = Convert.ToInt32(reader["user_id"]),
        //        Balance = Convert.ToDecimal(reader["balance"]),
        //    };

        //    return account;
        //}
    }
}

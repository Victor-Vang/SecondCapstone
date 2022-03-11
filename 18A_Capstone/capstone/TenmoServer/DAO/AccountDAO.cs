using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TenmoServer.Models;
using TenmoServer.Security;
using TenmoServer.Security.Models;

namespace TenmoServer.DAO
{
    public class AccountDao : IAccountDao
    {
        private readonly string connectionString;

        private string sqlGetAccountByUserId = "SELECT * FROM account WHERE user_id = @user_id;";

        private string sqlUpdateAccountBalances = "UPDATE account SET balance = (balance - @amount) WHERE account_id = @senderAccountId; " +
            "UPDATE account SET balance = (balance + @amount) WHERE account_id = @receiverAccountId;";
                                            
        public AccountDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Account GetAccountByUserId(int userId)
        {
            Account returnAccount = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                     
                    SqlCommand cmd = new SqlCommand(sqlGetAccountByUserId, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    
                    SqlDataReader reader = cmd.ExecuteReader();
                   
                    if (reader.Read())
                    {
                        returnAccount = GetAccountFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnAccount;
        }
        
        public bool UpdateAccountBalances(Transfer transfer)
        {
            Transfer updated = transfer;
            bool completed = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlUpdateAccountBalances, conn);
                    cmd.Parameters.AddWithValue("@amount", updated.Amount);
                    cmd.Parameters.AddWithValue("@senderAccountId", updated.AccountFrom);
                    cmd.Parameters.AddWithValue("@receiverAccountId", updated.AccountTo);

                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        completed = true;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return completed;
        }

        private Account GetAccountFromReader(SqlDataReader reader)
        {
            Account account = new Account()
            {
                AccountId = Convert.ToInt32(reader["account_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                Balance = Convert.ToDecimal(reader["balance"]),
            };
            return account;
        }
    }
}

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

        private string sqlGetAccount = "SELECT * FROM account WHERE user_id = @user_id;";

        private string sqlUpdateAccount = "UPDATE account" + 
                                            "SET balance = @balance, account_id = @accountId" +
                                            "WHERE user_id = @userId";
                                            
        public AccountDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Account GetAccount(int userId)
        {
            Account returnAccount = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetAccount, conn);
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
        public Account Update(Account updated)
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlUpdateAccount, conn);
                    cmd.Parameters.AddWithValue("@userId", updated.UserId);
                    cmd.Parameters.AddWithValue("@accountId", updated.AccountId);
                    cmd.Parameters.AddWithValue("@ballance", updated.Balance);
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        return updated;
                    }


                }
            }
            catch (SqlException)
            {
                throw;
            }
            
           
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

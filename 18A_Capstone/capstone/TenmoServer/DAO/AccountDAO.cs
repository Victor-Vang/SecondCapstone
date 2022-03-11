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

        private string sqlGetAccountByAccountId = "SELECT * FROM account WHERE account_id = @account_id;";

        private string sqlUpdateAccount = "UPDATE account SET account_id = @account_id, user_id = @user_id, balance = @balance WHERE account_id = @account_id;";

                                            
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
        
        public Account GetAccountByAccountId(int accountId)
        {
            Account returnAccount = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                     
                    SqlCommand cmd = new SqlCommand(sqlGetAccountByAccountId, conn);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    
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

        //private string sqlUpdateAccount = "UPDATE account SET account_id = @account_id, user_id = @user_id, balance = @balance WHERE account_id = @account_id;";
        public Account UpdateAccount(Account updated)
        {
            Account account = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlUpdateAccount, conn);
                    cmd.Parameters.AddWithValue("@userId", updated.UserId);
                    cmd.Parameters.AddWithValue("@accountId", updated.AccountId);
                    cmd.Parameters.AddWithValue("@balance", updated.Balance);

                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        account = updated;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return updated;
        }
        
        public Account UpdateAccountBalance(Transfer transfer)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlUpdateAccount, conn);
                    cmd.Parameters.AddWithValue("@userId", updated.UserId);
                    cmd.Parameters.AddWithValue("@accountId", updated.AccountId);
                    cmd.Parameters.AddWithValue("@balance", updated.Balance);

                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        account = updated;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return updated;
        }



        //public Account Update(Account updated)
        //{
            
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand(sqlUpdateAccount, conn);
        //            cmd.Parameters.AddWithValue("@userId", updated.UserId);
        //            cmd.Parameters.AddWithValue("@accountId", updated.AccountId);
        //            cmd.Parameters.AddWithValue("@balance", updated.Balance);
        //            int count = cmd.ExecuteNonQuery();
        //            if (count > 0)
        //            {
        //                return updated;
        //            }


        //        }
        //    }
        //    catch (SqlException)
        //    {
        //        throw;
        //    }
            
           
        //}

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

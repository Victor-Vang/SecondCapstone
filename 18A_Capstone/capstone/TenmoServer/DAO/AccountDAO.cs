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

        private string sqlGetAccountByUserId = " SELECT account_id, a.user_id, balance, username FROM account a JOIN tenmo_user tu ON a.user_id = tu.user_id WHERE a.user_id = @user_Id;";

        private string sqlUpdateAccountBalances = "UPDATE account SET balance = (balance - @amount) WHERE account_id = @senderAccountId; " +
            "UPDATE account SET balance = (balance + @amount) WHERE account_id = @receiverAccountId;";

        private string sqlGetAccounts = " SELECT account_id, a.user_id, username, balance FROM account a JOIN tenmo_user tu ON a.user_id = tu.user_id";
                                            
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
        
        public Transfer UpdateAccountBalances(Transfer transfer)
        {
            Transfer updated = null; ;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlUpdateAccountBalances, conn);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    cmd.Parameters.AddWithValue("@senderAccountId", transfer.AccountFrom);
                    cmd.Parameters.AddWithValue("@receiverAccountId", transfer.AccountTo);

                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        updated = transfer;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return updated;
        }

        public List<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetAccounts, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Account account = GetAccountFromReader(reader);
                        accounts.Add(account);
                    }
                }

            }
            catch (Exception ex)
            {
                accounts = new List<Account>();
            }
            return accounts;
        }

        private Account GetAccountFromReader(SqlDataReader reader)
        {
            Account account = new Account()
            {
                AccountId = Convert.ToInt32(reader["account_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                Balance = Convert.ToDecimal(reader["balance"]),
                Username = Convert.ToString(reader["username"])
            };
            return account;
        }
    }
}

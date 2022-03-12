using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoConsoleService : ConsoleService
    {
        private TenmoApiService tenmoApiService;
        /************************************************************
            Print methods
        ************************************************************/
        public void PrintLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Welcome to TEnmo!");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }

        public void PrintMainMenu(string username)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Hello, {username}!");
            Console.WriteLine("1: View your current balance");
            Console.WriteLine("2: View your past transfers");
            Console.WriteLine("3: View your pending requests");
            Console.WriteLine("4: Send TE bucks");
            Console.WriteLine("5: Request TE bucks");
            Console.WriteLine("6: Log out");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }

        public bool IsValidUserId(List<ApiUser> users, int receiverId, int userId)
        {
            if (users == null)
            {
                return false;
            }
            if (userId == receiverId)
            {
                return false;
            }

            foreach (ApiUser user in users)
            {
                if (user.UserId == receiverId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsValidBalance(decimal moneyToBeSent, Account sender)
        {

            if ((moneyToBeSent <= 0) || (moneyToBeSent > sender.Balance))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public void PrintUsers(List<ApiUser> users, int userId)
        {

            Console.WriteLine("------------Users--------------");
            Console.WriteLine(" Id   |   UserName");
            Console.WriteLine("--------------------------------");

            foreach (ApiUser user in users)

                if (user.UserId != userId)
                {
                    Console.WriteLine($"{user.UserId}  |    {user.Username}");
                }
            Console.WriteLine("--------------------------------");
        }


        public LoginUser PromptForLogin()
        {
            string username = PromptForString("User name");
            if (String.IsNullOrWhiteSpace(username))
            {
                return null;
            }
            string password = PromptForHiddenString("Password");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        // Add application-specific UI methods here...

        public int PromptForReceiverId(List<ApiUser> users, int userId)
        {
            PrintUsers(users, userId);
            Console.WriteLine();
            while (true)
            {
                int receiverId = PromptForInteger($"Id of the user you are sending to [0]");
                if (receiverId == 0)
                {
                    return 0;
                }


                if (IsValidUserId(users, receiverId, userId))
                {
                    return receiverId;
                }
                PrintError("This is not a valid user ID number.");
            }
        }

        public decimal PromptForMoneyAmount(Account sender)
        {
            Console.Write($"Enter amount to send: ");
            decimal moneyToBeSent = decimal.Parse(Console.ReadLine());

            if (IsValidBalance(moneyToBeSent, sender) == true)
            {
                return moneyToBeSent;
            }
            if (IsValidBalance(moneyToBeSent, sender) == false)
            {
                return -1;
            }
            return 0;
        }

        public void PrintTransfers(List<Transfer> transfers, int accountId, List<Account> accounts)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Transfers");
            Console.WriteLine("ID       From/To         Amount");
            Console.WriteLine("---------------------------");

            string username = "";

            foreach (Transfer transfer in transfers)
            {
                if (transfer.AccountTo == accountId)
                {
                    foreach (Account account in accounts)
                    {
                        if (account.AccountId == transfer.AccountFrom)
                        {
                            username = account.Username;
                        }
                    }
                    Console.WriteLine($"{transfer.TransferId}      From{username}      ${transfer.Amount}");
                }
                if (transfer.AccountFrom == accountId)
                {
                    foreach (Account account in accounts)
                    {
                        if (account.AccountId == transfer.AccountTo)
                        {
                            username = account.Username;
                        }
                    }
                    Console.WriteLine($"{transfer.TransferId}      To{username}      ${transfer.Amount}");
                }
                Console.WriteLine($" ");
            }
            Console.ReadLine();
        }
    }
}
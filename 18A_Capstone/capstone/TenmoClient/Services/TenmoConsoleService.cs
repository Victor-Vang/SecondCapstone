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

        public bool IsValidUserId(List<ApiUser> users, int receiverId)
        {
            if (users == null)
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


        public void PrintUsers(List<ApiUser> users)
        {
            Console.WriteLine("Id   |   UserName");

            foreach (ApiUser user in users)

                if (user.UserId != tenmoApiService.UserId)
                {
                    Console.WriteLine($"{user.UserId}  |    {user.Username}");
                }
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

        public int PromptForReceiverId(List<ApiUser> users)
        {
            PrintUsers(users);
            Console.WriteLine();
            while (true)
            {
                int receiverId = PromptForInteger($"Id of the user you are sending to [0]: ");
                if (receiverId == 0)
                {
                    return 0;
                }
                if (IsValidUserId(users, receiverId))
                {
                    return receiverId;
                }
                PrintError("This is not a valid user ID number.");
            }
        }

        public decimal PromptForMoneyAmount(Account sender, Account receiver)
        {
            Console.WriteLine($"Enter amount to send: ");
            decimal moneyToBeSent = Console.Read();

            if (IsValidBalance(moneyToBeSent, sender) == true)
            {

            }

            return 0.00M;
        }
    }
}
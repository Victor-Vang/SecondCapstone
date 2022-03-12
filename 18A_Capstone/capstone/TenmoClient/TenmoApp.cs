using System;
using System.Collections.Generic;
using TenmoClient.Models;
using TenmoClient.Services;

namespace TenmoClient
{
    public class TenmoApp
    {
        private readonly TenmoConsoleService console = new TenmoConsoleService();
        private readonly TenmoApiService tenmoApiService;

        public TenmoApp(string apiUrl)
        {
            tenmoApiService = new TenmoApiService(apiUrl);
        }

        public void Run()
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                // The menu changes depending on whether the user is logged in or not
                if (tenmoApiService.IsLoggedIn)
                {
                    keepGoing = RunAuthenticated();
                }
                else // User is not yet logged in
                {
                    keepGoing = RunUnauthenticated();
                }
            }
        }

        private bool RunUnauthenticated()
        {
            console.PrintLoginMenu();
            int menuSelection = console.PromptForInteger("Please choose an option", 0, 2, 1);
            while (true)
            {
                if (menuSelection == 0)
                {
                    return false;   // Exit the main menu loop
                }

                if (menuSelection == 1)
                {
                    // Log in
                    Login();
                    return true;    // Keep the main menu loop going
                }

                if (menuSelection == 2)
                {
                    // Register a new user
                    Register();
                    return true;    // Keep the main menu loop going
                }
                console.PrintError("Invalid selection. Please choose an option.");
                console.Pause();
            }
        }

        private bool RunAuthenticated()
        {
            console.PrintMainMenu(tenmoApiService.Username);
            int menuSelection = console.PromptForInteger("Please choose an option", 0, 6);
            if (menuSelection == 0)
            {
                // Exit the loop
                return false;
            }

            if (menuSelection == 1)
            {
                GetBalance();
                // View your current balance
            }

            if (menuSelection == 2)
            {
                ListTransfers();
                // View your past transfers
            }

            if (menuSelection == 3)
            {
                // View your pending requests
            }

            if (menuSelection == 4)
            {
                SendMoney();
            }

            if (menuSelection == 5)
            {
                // Request TE bucks
            }

            if (menuSelection == 6)
            {
                // Log out
                tenmoApiService.Logout();
                console.PrintSuccess("You are now logged out");
            }

            return true;    // Keep the main menu loop going
        }

        private void Login()
        {
            LoginUser loginUser = console.PromptForLogin();
            if (loginUser == null)
            {
                return;
            }

            try
            {
                ApiUser user = tenmoApiService.Login(loginUser);
                if (user == null)
                {
                    console.PrintError("Login failed.");
                }
                else
                {
                    console.PrintSuccess("You are now logged in");
                }
            }
            catch (Exception)
            {
                console.PrintError("Login failed.");
            }
            console.Pause();
        }

        private void Register()
        {
            LoginUser registerUser = console.PromptForLogin();
            if (registerUser == null)
            {
                return;
            }
            try
            {
                bool isRegistered = tenmoApiService.Register(registerUser);
                if (isRegistered)
                {
                    console.PrintSuccess("Registration was successful. Please log in.");
                }
                else
                {
                    console.PrintError("Registration was unsuccessful.");
                }
            }
            catch (Exception)
            {
                console.PrintError("Registration was unsuccessful.");
            }
            console.Pause();
        }
        public void GetBalance()
        {
            int userId = tenmoApiService.UserId;

            try
            {
                Account account = tenmoApiService.GetAccountByUserId(userId);

                if (account != null)
                {
                    Console.WriteLine($"Your current account balance is: {account.Balance.ToString("C")}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                console.PrintError("Unable to retrieve balance: " + ex.Message);
            }
            console.Pause();
        }

        public void SendMoney()
        {
            int userId = tenmoApiService.UserId;
            Transfer transfer = new Transfer();

            try
            {
                List<ApiUser> users = tenmoApiService.GetUsers();
                if (users != null)
                {
                    int receiverId = console.PromptForReceiverId(users, userId);
                    if (receiverId == 0)
                    {
                        return;
                    }
                    if (receiverId == -1)
                    {
                        console.PrintError("You can't send money to yourself. Come on, John..");
                        console.Pause();
                    }


                    Account sender = tenmoApiService.GetAccountByUserId(userId);
                    Account receiver = tenmoApiService.GetAccountByUserId(receiverId);

                    if (receiver != null)
                    {

                        decimal moneyToBeSent = console.PromptForMoneyAmount(sender);

                        if (moneyToBeSent == 0)
                        {
                            console.PrintError("Invalid amount.");
                            console.Pause();
                            return;
                        }
                        if (moneyToBeSent == -1)
                        {
                            console.PrintError("Insufficient funds in your account.");
                            console.Pause();
                            return;
                        }
                        else
                        {
                            transfer.Amount = moneyToBeSent;
                            transfer.AccountFrom = sender.AccountId;
                            transfer.AccountTo = receiver.AccountId;

                            tenmoApiService.UpdateAccountBalances(transfer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Error: " + ex.Message);
            }
            tenmoApiService.AddTransfer(transfer);

            console.Pause();
        }
        public void ListTransfers()
        {
            List<Account> accounts = tenmoApiService.GetAccounts();
            Account user = tenmoApiService.GetAccountByUserId(tenmoApiService.UserId);
            List<Transfer> transfers = tenmoApiService.GetTransfers(user.AccountId);

            if (transfers != null)
            {
                int userId = tenmoApiService.UserId;
                console.PrintTransfers(transfers, user.AccountId, accounts);
                console.PrintTransferDetails(transfers, accounts, user.AccountId, tenmoApiService.Username);
            }
            console.Pause();
        }
    }
}

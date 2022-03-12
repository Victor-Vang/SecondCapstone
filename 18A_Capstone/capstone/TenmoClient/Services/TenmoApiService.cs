using RestSharp;
using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }


        // Add methods to call api here...
        public Account GetAccountByUserId(int userId)
        {
            RestRequest request = new RestRequest($"account/{userId}");
            IRestResponse<Account> response = client.Get<Account>(request);

            CheckForError(response);

            return response.Data;
        }

        public List<ApiUser> GetUsers()
        {
            List<ApiUser> users = new List<ApiUser>();
            RestRequest request = new RestRequest("user");
            IRestResponse<List<ApiUser>> response = client.Get<List<ApiUser>>(request);

            CheckForError(response);

            return response.Data;
        }

        public Transfer AddTransfer(Transfer newTransfer)
        {
            RestRequest request = new RestRequest("transfer");
            request.AddJsonBody(newTransfer);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);

            CheckForError(response);

            return response.Data;
        }

        public void UpdateAccountBalances(Transfer transfer)
        {
            RestRequest request = new RestRequest("account");
            request.AddJsonBody(transfer);
            IRestResponse<Transfer> response = client.Put<Transfer>(request);

            CheckForError(response);
        }

        public List<Transfer> GetTransfers(int accountId)
        {
            List<Transfer> transfers = new List<Transfer>();
            RestRequest request = new RestRequest($"transfer/{accountId}");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

            CheckForError(response);

            return response.Data;
        }
        public List<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();
            RestRequest request = new RestRequest("account");
            IRestResponse<List<Account>> response = client.Get<List<Account>>(request);

            CheckForError(response);

            return response.Data;
        }
    }
}

using RestSharp;
using System.Collections.Generic;
using TenmoClient.Models;
using System;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        //Add methods to call api here...

        public Account GetAccount()
        {
            RestRequest request = new RestRequest(ApiUrl);
            IRestResponse<Account> response = client.Get<Account>(request);

            CheckResult(response);

            return response.Data;
        }

        private void CheckResult(IRestResponse<Account> response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if ((int)response.StatusCode == 401)
            {
                throw new Exception("Error occurred - user not authorized - 401.");
            }
            else if ((int)response.StatusCode == 403)
            {
                throw new Exception("Error occurred - user forbidden - 403");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
        }

        //protected void CheckForError(IRestResponse response)
        //{
        //    string message;
        //    if (response.ResponseStatus != ResponseStatus.Completed)
        //    {
        //        message = $"Error occurred - unable to reach server. Response status was '{response.ResponseStatus}'.";
        //        throw new HttpRequestException(message, response.ErrorException);
        //    }
        //    else if (!response.IsSuccessful)
        //    {
        //        if (response.StatusCode == HttpStatusCode.Unauthorized)
        //        {
        //            message = $"Authorization is required and the user has not logged in.";
        //        }
        //        else if (response.StatusCode == HttpStatusCode.Forbidden)
        //        {
        //            message = $"The user does not have permission.";
        //        }
        //        else
        //        {
        //            message = $"An http error occurred. Status code {(int)response.StatusCode} {response.StatusDescription}";
        //        }
        //        throw new HttpRequestException(message, response.ErrorException);
        //    }
        //}
    }
}

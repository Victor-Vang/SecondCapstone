using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDao accountDAO;
        public AccountController(IAccountDao accountDAO)
        {
            this.accountDAO = accountDAO;
        }

        // GET: /account/userId
        [HttpGet("{userId}")]
        public ActionResult<Account> GetAccountByUserId(int userId)
        {
            Account account = accountDAO.GetAccountByUserId(userId);

            if (account == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(account);
            }
        }
        
        // GET: /account/accountId
        //[HttpGet("{accountId}")]
        //public ActionResult<Account> GetAccountByAccountId(int accountId)
        //{
        //    Account account = accountDAO.GetAccountByAccountId(accountId);

        //    if (account == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(account);
        //    }
        //}

        [HttpPut("{accountId}")]
        public ActionResult UpdateSender(Transfer transfer)
        {
          accountDAO.UpdateAccountBalance()
        }
        
        //[HttpPut("{accountId}")]
        //public ActionResult UpdateSender(Transfer transfer)
        //{
        //    Account existingSender = accountDAO.GetAccountByAccountId(transfer.AccountFrom);

        //    if (existingSender == null)
        //    {
        //        return NotFound();
        //    }

        //    existingSender.Balance -= transfer.Amount;
        //    Account result = accountDAO.UpdateAccount(existingSender);
            
        //    Account existingReceiver = accountDAO.GetAccountByAccountId(transfer.AccountTo);

        //    if (existingReceiver == null)
        //    {
        //        return NotFound();
        //    }

        //    existingReceiver.Balance += transfer.Amount;
        //    Account secondResult = accountDAO.UpdateAccount(existingReceiver);

        //    return Ok();
        //}
    }
}

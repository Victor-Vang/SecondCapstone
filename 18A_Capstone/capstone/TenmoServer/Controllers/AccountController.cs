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
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDao accountDAO;
        public AccountController(IAccountDao accountDAO)
        {
            this.accountDAO = accountDAO;
        }

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
        [HttpGet]
        public ActionResult<List<Account>> GetAccounts()
        {
            List<Account> accounts = accountDAO.GetAccounts();
            if (accounts.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(accounts);
            }
        }

        [HttpPut]
        public ActionResult<Transfer> UpdateSender(Transfer transfer)
        {
            Transfer updated = accountDAO.UpdateAccountBalances(transfer);

            if (updated != null)
            {
                return Ok(updated);
            }
            else
            {
                return NotFound(updated);
            }

        }
    }
}

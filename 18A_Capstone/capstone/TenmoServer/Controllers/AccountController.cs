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

        [HttpPut("{accountId}")]
        public ActionResult<bool> UpdateSender(Transfer transfer)
        {
            bool result = accountDAO.UpdateAccountBalances(transfer);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }
    }
}

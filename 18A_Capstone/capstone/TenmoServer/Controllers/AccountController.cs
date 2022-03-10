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
        public ActionResult<Account> GetAccount(int userId)
        {
            Account account = accountDAO.GetAccount(userId);

            if (account == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(account);
            }
        }

        [HttpPut("{userId}")]
        public ActionResult<Account> UpdateAccount(int userId, Account account)
        {
            Account existingAccount = accountDAO.GetAccount(userId);
            if (existingAccount == null)
            {
                return NotFound();
            }
            Account result = accountDAO.UpdateAccount(account);
            return Ok(result);
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDAO accountDAO;
        public AccountController(IAccountDAO accountDAO)
        {
            this.accountDAO = accountDAO;
        }

        // GET: /account
        [HttpGet]
        public ActionResult<Account> GetAccount()
        {
            Account account = accountDAO.GetAccount();
            if (account == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(account);
            }
        }
    }
}

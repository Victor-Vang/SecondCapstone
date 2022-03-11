﻿using Microsoft.AspNetCore.Authorization;
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
    public class TransferController : ControllerBase
    {

        private ITransferDao transferDao;

        public TransferController(ITransferDao transferDao)
        {
            this.transferDao = transferDao;
        }

        [HttpPost]
        public ActionResult<bool> AddTransfer(Transfer transfer)
        {
            bool result = transferDao.AddTransfer(transfer);

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

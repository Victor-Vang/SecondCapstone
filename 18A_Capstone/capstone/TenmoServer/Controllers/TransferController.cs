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
    public class TransferController : ControllerBase
    {

        private ITransferDao transferDao;

        public TransferController(ITransferDao transferDao)
        {
            this.transferDao = transferDao;
        }

        [HttpPost]
        public ActionResult<Transfer> AddTransfer(Transfer transfer)
        {
            Transfer result = transferDao.AddTransfer(transfer);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<List<Transfer>> GetTransfers(int id)
        {
            List<Transfer> transfers = transferDao.GetTransfers(id);
            if (transfers.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(transfers);
            }
        }
    }
}

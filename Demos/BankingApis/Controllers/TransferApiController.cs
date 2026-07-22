using Microsoft.AspNetCore.Mvc;
using BankingApis.Services;
using BankingApis.DTOs;

namespace BankingApis.Controllers
{
    [ApiController]
    [Route("api/transfers")]
    public class TransferController : ControllerBase
    {
        private readonly IAccountService _service;

        public TransferController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> TransferFunds(
            TransferRequest request)
        {
            var result =
                await _service.TransferFunds(request);

            if (!result)
                return BadRequest("Transfer Failed");

            return Ok("Transfer Successful");
        }
    }
}
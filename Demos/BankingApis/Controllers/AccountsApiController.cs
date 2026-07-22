using Microsoft.AspNetCore.Mvc;
using BankingApis.Services;

namespace BankingApis.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountsController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet("{accountNumber}/balance")]
        public async Task<IActionResult> GetBalance(
            string accountNumber)
        {
            return Ok(await _service.GetBalance(accountNumber));
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace BankingLoanApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanApiController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoanApiController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet("{id}")]
        public IActionResult GetLoan(int id)
        {

            return Ok(new
            {
                LoanId = id,
                Customer = "James",
                Amount = 1000000,
                Status = "Approved",
                CS = _config["SqlConnection"]
            });
        }
    }
}

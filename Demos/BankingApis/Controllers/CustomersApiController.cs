using Microsoft.AspNetCore.Mvc;
using BankingApis.Services;
using BankingApis.DTOs;

namespace BankingApis.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;


        public CustomersController(
            ICustomerService service, IConfiguration config, ILogger logger)
        {
            _service = service;
            _config = config;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResponse>>
            CreateCustomer(
                CreateCustomerRequest request)
        {
            return Ok(
                await _service.CreateCustomerAsync(
                    request));
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerResponse>>>
            GetCustomers()
        {
            string connectString = _config.GetConnectionString("cn")!;

            _logger.LogInformation($"The connection string is: {connectString}");

            return Ok(
                await _service.GetCustomersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>>
            GetCustomer(Guid id)
        {
            return Ok(
                await _service.GetCustomerAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerResponse>>
            UpdateCustomer(
                Guid id,
                UpdateCustomerRequest request)
        {
            return Ok(
                await _service.UpdateCustomerAsync(
                    id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            DeleteCustomer(Guid id)
        {
            await _service.DeleteCustomerAsync(id);

            return NoContent();
        }

        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            return Ok(
                await _service.ActivateCustomerAsync(id));
        }

        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            return Ok(
                await _service.DeactivateCustomerAsync(id));
        }

        // [HttpPatch("{id}/kyc/approve")]
        // public async Task<IActionResult> ApproveKyc(Guid id)
        // {
        //     return Ok(
        //         await _service.ApproveKycAsync(id));
        // }

        // [HttpPatch("{id}/kyc/reject")]
        // public async Task<IActionResult> RejectKyc(Guid id)
        // {
        //     return Ok(
        //         await _service.RejectKycAsync(id));
        // }
    }
}
using DemoCosmosDB.Models;
using DemoCosmosDB.Repositories;
using DemoCosmosDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoCosmosDB.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private readonly IBankAccountRepository _repository;
        private readonly BankAccountService _service;

        public BankAccountsController(
            IBankAccountRepository repository,
            BankAccountService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            BankAccount account)
        {
            var created =
                await _repository.CreateAsync(account);

            return Ok(created);
        }

        [HttpGet("{customerId}/{accountId}")]
        public async Task<IActionResult> Get(
            string customerId,
            string accountId)
        {
            var account =
                await _repository.GetAsync(
                    accountId,
                    customerId);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            BankAccount account)
        {
            return Ok(
                await _repository.UpdateAsync(account));
        }

        [HttpDelete("{customerId}/{accountId}")]
        public async Task<IActionResult> Delete(
            string customerId,
            string accountId)
        {
            await _repository.DeleteAsync(
                accountId,
                customerId);

            return NoContent();
        }

        [HttpPost("{customerId}/{accountId}/deposit")]
        public async Task<IActionResult> Deposit(
            string customerId,
            string accountId,
            decimal amount)
        {
            await _service.DepositAsync(
                accountId,
                customerId,
                amount);

            return Ok("Deposit Successful");
        }

        [HttpPost("{customerId}/{accountId}/withdraw")]
        public async Task<IActionResult> Withdraw(
            string customerId,
            string accountId,
            decimal amount)
        {
            await _service.WithdrawAsync(
                accountId,
                customerId,
                amount);

            return Ok("Withdrawal Successful");
        }
    }
}
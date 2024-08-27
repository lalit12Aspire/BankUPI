using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UPIWork.Repository.Service;
using UPIWork.Models.Entity;

namespace UPIWork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(IAccountRepository accountRepository, ITransactionRepository transactionRepository,ILogger<TransactionController> logger)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _logger=logger;
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance(string upiId = null, string accountNumber = null)
        {
            try
            {
                var balance = await _transactionRepository.GetBalanceByUPIOrAccountNumberAsync(upiId, accountNumber);
                return Ok(balance);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("mini-statement")]
        public async Task<IActionResult> GetMiniStatement(string upiId = null, string accountNumber = null, int numberOfTransactions = 5)
        {
            try
            {
                var transactions = await _transactionRepository.GetMiniStatementByUPIOrAccountNumberAsync(upiId, accountNumber, numberOfTransactions);
                return Ok(transactions);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("transaction-count")]
        public async Task<IActionResult> GetTransactionCount(string upiId = null, string accountNumber = null)
        {
            try
            {
                var count = await _transactionRepository.GetTransactionCountByUPIOrAccountNumberAsync(upiId, accountNumber);
                return Ok(count);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deposit/{accountId}")]
        public async Task<IActionResult> Deposit(Guid accountId, [FromBody] decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest("Deposit amount must be greater than zero.");
            }
           
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            account.Balance += amount;
             _logger.LogInformation($"Deposit of {amount} made to account {accountId}");
            await _accountRepository.UpdateAsync(account);

            var transaction = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                AccountID = accountId,
                AmountToBeDeposited = amount,
                TransactionDate = DateTime.UtcNow,
                //TransactionType = "Deposit"
            };

            await _transactionRepository.AddAsync(transaction);

            return Ok("Deposit successful.");
        }

        [HttpPost("withdraw/{accountId}")]
        public async Task<IActionResult> Withdraw(Guid accountId, [FromBody] decimal amount)
        {
            if (amount <= 0)
            {
                return BadRequest("Withdrawal amount must be greater than zero.");
            }

            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            if (account.Balance < amount)
            {
                return BadRequest("Insufficient balance.");
            }

            account.Balance -= amount;
             _logger.LogInformation($"Withdrawal of {amount} made from account {accountId}");
            await _accountRepository.UpdateAsync(account);

            var transaction = new Transaction
            {
                TransactionID = Guid.NewGuid(),
                AccountID = accountId,
                AmountToBeDeposited = amount,
                TransactionDate = DateTime.UtcNow,
                //TransactionType = "Withdrawal"
            };

            await _transactionRepository.AddAsync(transaction);

            return Ok("Withdrawal successful.");
        }
    }
}

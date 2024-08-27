using Microsoft.AspNetCore.Mvc;
using UPIWork.Models.DTO;
using UPIWork.Repository.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UPIWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        // GET: api/account
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            _logger.LogInformation("Fetching all accounts.");
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccountById(Guid accountId)
        {
            _logger.LogInformation("Fetching account with ID {CustomerId}.", accountId);
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                _logger.LogWarning("Account with ID {CustomerId} not found.", accountId);
                return NotFound("Account not found.");
            }
            _logger.LogInformation("Account with ID {CustomerId} found.", accountId);
            return Ok(account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid accountId, [FromBody] AccountDTO accountDTO)
        {
            if (accountDTO == null || accountId != accountDTO.CustomerId)
            {
                _logger.LogWarning("Invalid account data or ID mismatch.");
                return BadRequest("Account data is invalid.");
            }
            _logger.LogInformation("Updating account with ID {AccountId}.", accountId);

            await _accountRepository.UpdateAccountAsync(accountId, accountDTO);
            _logger.LogInformation("Account with ID {AccountId} updated successfully.", accountId);
            return NoContent(); // No content to return after successful update
        }

        // DELETE: api/account/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid accountId)
        {
            _logger.LogInformation("Deleting account with ID {AccountId}.", accountId);
            await _accountRepository.DeleteAccountAsync(accountId);
            _logger.LogInformation("Account with ID {AccountId} deleted successfully.", accountId);
            return NoContent(); // No content to return after successful deletion
        }

    }
}

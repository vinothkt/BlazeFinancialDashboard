using BlazeMyFinance.Server.Service;
using BlazeMyFinance.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BlazeMyFinance.Server.Api.Controllers
{
    /// <summary>
    /// Front End API - to invoke external APIs which are protected/secured
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IAccountService? _accountService;
        private readonly ITransactionService? _transactionService;

        public DashboardController(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        /// <summary>
        /// Get a particular account information
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet("getaccountinfo/{accountId}")]
        public async Task<IActionResult> GetAccountInfo(int accountId)
        {
            //Call both services asynchronously
            var balanceTask = _accountService?.GetBalanceAsync(accountId);
            var transactionTask = _transactionService?.GetTransactionsAsync(accountId);

            //wait for both services to finish
            await Task.WhenAll(balanceTask, transactionTask);

            var result = new CustomerInfo()
            {
                AccountInfo = balanceTask.Result.Value,
                Transactions = transactionTask.Result.Value
            };
            return Ok(result);
        }

        /// <summary>
        /// Get all customers information
        /// </summary>
        /// <returns></returns>
        [HttpGet("getallcustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _accountService?.GetAllCustomersAsync();
            if (result != null && result.Value != null)
            {
                DashboardInfo dashboardInfo = new DashboardInfo() { Customers = result.Value };
                return Ok(dashboardInfo);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update Account information
        /// </summary>
        /// <param name="accountInfo"></param>
        /// <returns></returns>
        [HttpPost("updateaccountinfo")]
        public async Task<IActionResult> UpdateAccountInfo(AccountInfo accountInfo)
        {
            var result = await _accountService?.UpdateAccountInfoAsync(accountInfo);
            if (result != null && result.Value != null)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}

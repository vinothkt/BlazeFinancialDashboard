using BlazeMyFinance.Shared.Common;
using BlazeMyFinance.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BlazeMyFinance.API.Controllers
{
    /// <summary>
    /// Account API controller handles all account related API methods 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    //[Authorize] - enable  to have this API secured; & make sure HTTP request for this API, sends with token
    public class AccountController : ControllerBase
    {
        //Use logger to capture trace/errors/events
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// GET getallcustomers
        /// Purpose: This method gets all accounts from database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getallcustomers")]
        [ProducesResponseType<ApiResult<List<CustomerInfo>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiResult<List<CustomerInfo>>>(StatusCodes.Status400BadRequest)]
        public async Task<ApiResult<List<CustomerInfo>>> GetAllCustomers()
        {
            ApiResult<List<CustomerInfo>> apiResult = new();
            try
            {
                //artificial delay to mimic real time processing
                await Task.Delay(1500);

                //here: you can have your business service/layer which can do logical processing of data coming from data service/layer, deliver them in a list.
                //until then, assume balance is hard coded
                Random random = new Random();
                //build up some names for account
                string[] prefixes = { "Alpha", "Beta", "Gamma", "Theta", "Delta", "Omega", "Sigma", "Zeta", "Hexa", "Octa" };
                string[] suffixes = { "LLC", "Tech", "Doc", "System", "Java", "Manufacter", "Corp", "Inc", "FinTech", "Apparel" };
                var accountsList = Enumerable.Range(1, 50)
                    .Select(id => new CustomerInfo
                    {
                        CustomerId = id,
                        AccountInfo = new AccountInfo() { CustomerId = id, AccountNumber = random.Next(1,9999), IsActive = true },
                        CustomerName = $"{prefixes[random.Next(prefixes.Length)]}{id},{suffixes[random.Next(suffixes.Length)]}"
                    }).ToList();

                if (accountsList != null && accountsList.Any())
                {
                    apiResult.Success = true;
                    apiResult.Value = accountsList;
                    apiResult.Messages.Add($"Total {accountsList.Count} accounts available");
                }
                else
                {
                    apiResult.Success = false;
                    apiResult.Messages.Add($"There is no account available.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResult.Success = false;
                apiResult.Messages.Add($"GetAllAccounts-API: {ex.Message}");
            }
            return apiResult;
        }

        /// <summary>
        /// GET getbalance/1000001
        /// Purpose: This method accepts account id input and provides balance information if given account is valid and exists
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getbalance/{accountId}")]
        [ProducesResponseType<ApiResult<AccountInfo>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiResult<AccountInfo>>(StatusCodes.Status400BadRequest)]
        public async Task<ApiResult<AccountInfo>> GetBalance(long accountId)
        {
            ApiResult<AccountInfo> apiResult = new();
            try
            {
                if (accountId > 0)
                {
                    //artificial delay to mimic real time processing
                    await Task.Delay(1000);

                    //here: you can have your business service/layer which can do logical processing of data coming from data service/layer.
                    //until then, assume balance is hard coded
                    Random random = new Random();
                    apiResult.Success = true;
                    apiResult.Value = new AccountInfo
                    {
                        AccountNumber = accountId,
                        RemainingBalance = Convert.ToDecimal(random.Next(0, 9999999))
                    };
                }
                else
                {
                    apiResult.Success = false;
                    apiResult.Messages.Add($"Invalid Account Id passed.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResult.Success = false;
                apiResult.Messages.Add($"GetBalance-API: {ex.Message}");
            }
            return apiResult;
        }

        /// <summary>
        /// POST: updateaccountinfo
        /// Purpose: This method accepts account object and update values
        /// </summary>
        /// <param name="accountInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateaccountinfo")]
        [ProducesResponseType<ApiResult<AccountInfo>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiResult<AccountInfo>>(StatusCodes.Status400BadRequest)]
        public async Task<ApiResult<AccountInfo>> UpdateAccountInfo([FromBody]AccountInfo accountInfo)
        {
            ApiResult<AccountInfo> apiResult = new();
            try
            {
                if (accountInfo != null && accountInfo.AccountNumber > 0)
                {
                    //artificial delay to mimic real time processing
                    await Task.Delay(1000);
                    //here: you can have your business service/layer which can do logical processing of data coming from data service/layer.
                    //just flip the account status
                    accountInfo.IsActive = (accountInfo.IsActive.HasValue && !accountInfo.IsActive.Value);
                    apiResult.Success = true;
                    apiResult.Value = accountInfo;
                }
                else
                {
                    apiResult.Success = false;
                    apiResult.Messages.Add($"Invalid Account information passed.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                apiResult.Success = false;
                apiResult.Messages.Add($"UpdateAccountInfo-API: {ex.Message}");
            }
            return apiResult;
        }
    }
}

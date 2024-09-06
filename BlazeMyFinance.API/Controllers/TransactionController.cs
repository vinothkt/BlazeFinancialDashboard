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
    //[Authorize] - secure the API 
    public class TransactionController : ControllerBase
    {
        //Use logger to capture trace/errors/events
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// GET GetTransactions/1000001
        /// Purpose: This method accepts account id input and provides balance information if given account is valid and exists
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTransactions/{accountId}")]
        [ProducesResponseType<ApiResult<List<TransactionInfo>>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiResult<List<TransactionInfo>>>(StatusCodes.Status400BadRequest)]
        public async Task<ApiResult<List<TransactionInfo>>> GetTransactions(long accountId)
        {
            ApiResult<List<TransactionInfo>> apiResult = new();
            try
            {
                if (accountId > 0)
                {
                    //artificial delay to mimic real time processing
                    await Task.Delay(1000);

                    //here: you can have your business service/layer which can do logical processing of data coming from data service/layer, deliver them in a list.
                    //until then, assume balance is hard coded
                    Random random = new Random();
                    var transactionsList = Enumerable.Range(1, 10)
                    .Select(id => new TransactionInfo
                    {
                        AccountId = accountId,
                        Amount = random.Next(1, 9999),
                        RemainingBalance = random.Next(1, 99999)
                    }).ToList();

                    if (transactionsList != null && transactionsList.Any())
                    {
                        apiResult.Success = true;
                        apiResult.Value = transactionsList;
                        apiResult.Messages.Add($"Total {transactionsList.Count} transactions available");
                    }
                    else
                    {
                        apiResult.Success = false;
                        apiResult.Messages.Add($"There is no transaction available.");
                    }
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
                apiResult.Messages.Add($"GetTransactions-API: {ex.Message}");
            }
            return apiResult;
        }

    }
}

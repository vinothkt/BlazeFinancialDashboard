using BlazeMyFinance.Shared.Common;
using BlazeMyFinance.Shared.Dto;
using BlazeMyFinance.Shared.Helper;
using Microsoft.Extensions.Options;

namespace BlazeMyFinance.Server.Service
{

    public interface ITransactionService
    {
        Task<ApiResult<List<TransactionInfo>>> GetTransactionsAsync(long accountId);
    }

    public class TransactionService : ITransactionService
    {
        private readonly IHttpFactory _httpFactory;
        public IOptions<ApiEndpoint> _endPoints { get; set; }

        public TransactionService(IHttpFactory httpFactory, IOptions<ApiEndpoint> endpoints)
        {
            _httpFactory = httpFactory;
            _endPoints = endpoints;
        }

        /// <summary>
        /// GetTransactionsAsync
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<TransactionInfo>>> GetTransactionsAsync(long accountId)
        {
            ApiResult<List<TransactionInfo>> apiResult = new();
            try
            {
                var uri =  string.Format(_endPoints.Value.GetTransactions, accountId);
                var response = await _httpFactory.GetApiCall<ApiResult<List<TransactionInfo>>>(uri);
                if (response != null)
                {
                    apiResult = response;
                }
                else
                {
                    apiResult.Success = false;
                }
            }
            catch (Exception ex)
            {
                //handle error
            }
            return apiResult;
        }
    }
}

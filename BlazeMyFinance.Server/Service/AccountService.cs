using BlazeMyFinance.Shared.Common;
using BlazeMyFinance.Shared.Dto;
using BlazeMyFinance.Shared.Helper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BlazeMyFinance.Server.Service
{

    public interface IAccountService
    {
        Task<ApiResult<List<CustomerInfo>>> GetAllCustomersAsync();
        Task<ApiResult<AccountInfo>> GetBalanceAsync(long accountId);
        Task<ApiResult<AccountInfo>> UpdateAccountInfoAsync(AccountInfo accountInfo);
    }

    public class AccountService : IAccountService
    {
        private readonly IHttpFactory _httpFactory;
        public IOptions<ApiEndpoint> _endPoints { get; set; }

        public AccountService(IHttpFactory httpFactory, IOptions<ApiEndpoint> endpoints)
        {
            _httpFactory = httpFactory;
            _endPoints = endpoints;
        }

        /// <summary>
        /// GetAllCustomersAsync
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<CustomerInfo>>> GetAllCustomersAsync()
        {
            ApiResult<List<CustomerInfo>> apiResult = new();
            try
            {
                var uri = _endPoints.Value.GetAllCustomers;
                var response = await _httpFactory.GetApiCall<ApiResult<List<CustomerInfo>>>(uri);
                if(response != null)
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

        /// <summary>
        /// GetBalanceAsync
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<ApiResult<AccountInfo>> GetBalanceAsync(long accountId)
        {
            ApiResult<AccountInfo> apiResult = new();
            try
            {
                var uri =  string.Format(_endPoints.Value.GetBalance, accountId);
                var response = await _httpFactory.GetApiCall<ApiResult<AccountInfo>>(uri);
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

        /// <summary>
        /// UpdateAccountInfoAsync
        /// </summary>
        /// <param name="accountInfo"></param>
        /// <returns></returns>
        public async Task<ApiResult<AccountInfo>> UpdateAccountInfoAsync(AccountInfo accountInfo)
        {
            ApiResult<AccountInfo> apiResult = new();
            try
            {
                var uri = _endPoints.Value.UpdateAccountInfo;
                var serialized = JsonConvert.SerializeObject(accountInfo);
                var response = await _httpFactory.PostApiCall<ApiResult<AccountInfo>>(uri, serialized);
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

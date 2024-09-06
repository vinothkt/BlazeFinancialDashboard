using BlazeMyFinance.Shared.Common;
using BlazeMyFinance.Shared.Dto;
using BlazeMyFinance.Shared.Helper;
using Microsoft.Extensions.Options;
using Xunit;


namespace BlazeMyFinance.Server.Service.UnitTest
{
    public class AccountServiceTest
    {
        private readonly IAccountService _service;
        private IOptions<ApiEndpoint> _endpoints;

        public AccountServiceTest() {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.Development.json")
                .AddEnvironmentVariables()
                .Build();
            ApiEndpoint apiEndpoint = new();
            _endpoints = Options.Create(apiEndpoint);
            config.GetSection("Endpoints").Bind(apiEndpoint);
            var services = new ServiceCollection();
            services.AddTransient<IHttpFactory, HttpFactory>();
            services.AddTransient<IAccountService, AccountService>();
            var http = new HttpFactory();
            _service = new AccountService(http, _endpoints);

        }

        [Fact]
        public async Task GetAllAccountsTest()
        {
            //Arrange

            //Act
            var result = await _service.GetAllAccountsAsync();

            //Assert
            Assert.Equal(true, result != null);
        }

        [Fact]
        public async Task GetBalanceTest()
        {
            //Arrange

            //Act
            var result = await _service.GetBalanceAsync(1);

            //Assert
            Assert.Equal(true, result != null);
        }

        [Fact]
        public async Task UpdateAccountInfoTest()
        {
            //Arrange

            //Act
            var result = await _service.UpdateAccountInfoAsync(new AccountInfo() { AccountId = 2, IsActive = false });

            //Assert
            Assert.Equal(true, result != null);
        }
    }
}

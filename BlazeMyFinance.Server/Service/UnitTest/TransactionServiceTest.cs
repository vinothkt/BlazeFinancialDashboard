using BlazeMyFinance.Shared.Common;
using BlazeMyFinance.Shared.Helper;
using Microsoft.Extensions.Options;
using Xunit;


namespace BlazeMyFinance.Server.Service.UnitTest
{
    public class TransactionServiceTest
    {
        private readonly ITransactionService _service;
        private IOptions<ApiEndpoint> _endpoints;

        public TransactionServiceTest() {
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
            services.AddTransient<ITransactionService, TransactionService>();
            var http = new HttpFactory();
            _service = new TransactionService(http, _endpoints);

        }

        [Fact]
        public async Task GetTransactionsTest()
        {
            //Arrange

            //Act
            var result = await _service.GetTransactionsAsync(1);

            //Assert
            Assert.Equal(true, result != null);
        }
    }
}

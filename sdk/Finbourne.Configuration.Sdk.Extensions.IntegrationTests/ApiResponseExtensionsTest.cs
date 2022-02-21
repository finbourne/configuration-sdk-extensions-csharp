using Finbourne.Configuration.Sdk.Api;
using NUnit.Framework;

namespace Finbourne.Configuration.Sdk.Extensions.IntegrationTests
{
    [TestFixture]
    public class ApiResponseExtensionsTest
    {
        private IApiFactory _factory;
        private const string RequestIdRegexPattern = "[a-zA-Z0-9]{13}:[0-9a-fA-F]{8}";

        [OneTimeSetUp]
        public void SetUp()
        {
            _factory = IntegrationTestApiFactoryBuilder.CreateApiFactory("secrets.json");
        }

        [Test]
        public void GetRequestId_CanExtract_RequestId()
        {
            var apiResponse = _factory.Api<ConfigurationSetsApi>().ListConfigurationSetsWithHttpInfo("Personal");
            var requestId = apiResponse.GetRequestId();
            StringAssert.IsMatch(RequestIdRegexPattern, requestId);
        }

        [Test]
        public void GetRequestId_MissingHeader_ReturnsNull_RequestId()
        {
            var apiResponse = _factory.Api<ConfigurationSetsApi>().ListConfigurationSetsWithHttpInfo("Personal");
            // Remove header containing access token
            apiResponse.Headers.Remove(ApiResponseExtensions.RequestIdHeader);
            var requestId = apiResponse.GetRequestId();
            Assert.That(requestId, Is.Null);
        }

        [Test]
        public void GetRequestDateTime_CanExtract_DateHeader()
        {
            var apiResponse = _factory.Api<ConfigurationSetsApi>().ListConfigurationSetsWithHttpInfo("Personal");
            var date = apiResponse.GetRequestDateTime();
            Assert.IsNotNull(date);
        }

        [Test]
        public void GetRequestDateTime_InvalidDateHeader_ReturnsNull_DateHeader()
        {
            var apiResponse = _factory.Api<ConfigurationSetsApi>().ListConfigurationSetsWithHttpInfo("Personal");
            // Invalidate header containing access token
            apiResponse.Headers[ApiResponseExtensions.DateHeader] = new[] { "invalid" };
            var date = apiResponse.GetRequestDateTime();
            Assert.IsNull(date);
        }

        [Test]
        public void GetRequestDateTime_MissingHeader_ReturnsNull_DateHeader()
        {
            var apiResponse = _factory.Api<ConfigurationSetsApi>().ListConfigurationSetsWithHttpInfo("Personal");
            // Remove header containing access token
            apiResponse.Headers.Remove(ApiResponseExtensions.DateHeader);
            var date = apiResponse.GetRequestDateTime();
            Assert.IsNull(date);
        }
    }
}
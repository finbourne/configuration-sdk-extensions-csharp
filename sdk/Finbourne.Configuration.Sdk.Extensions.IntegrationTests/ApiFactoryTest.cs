using Finbourne.Configuration.Sdk.Api;
using Finbourne.Configuration.Sdk.Client;
using Finbourne.Configuration.Sdk.Model;
using NUnit.Framework;
using System;

namespace Finbourne.Configuration.Sdk.Extensions.IntegrationTests
{
    public class ApiFactoryTest
    {
        private IApiFactory _factory;

        [OneTimeSetUp]
        public void SetUp()
        {
            _factory = IntegrationTestApiFactoryBuilder.CreateApiFactory("secrets.json");
        }

        [Test]
        public void Create_ApplicationMetadataApi()
        {
            var api = _factory.Api<ApplicationMetadataApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<ApplicationMetadataApi>());
        }

        [Test]
        public void Create_ConfigurationSetsApi()
        {
            var api = _factory.Api<ConfigurationSetsApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<ConfigurationSetsApi>());
        }

        [Test]
        public void Api_From_IApplicationMetadataApi()
        {
            var api = _factory.Api<IApplicationMetadataApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<ApplicationMetadataApi>());
        }

        [Test]
        public void Api_From_IConfigurationSetsApi()
        {
            var api = _factory.Api<IConfigurationSetsApi>();

            Assert.That(api, Is.Not.Null);
            Assert.That(api, Is.InstanceOf<ConfigurationSetsApi>());
        }

        [Test]
        public void NetworkConnectivityErrors_ThrowsException()
        {
            var apiConfig = ApiConfigurationBuilder.Build("secrets.json");
            // nothing should be listening on this, so we should get a "No connection could be made" error...
            apiConfig.ConfigurationUrl = "https://localhost:56789/insights"; 

            var factory = new ApiFactory(apiConfig);
            var api = factory.Api<IConfigurationSetsApi>();

            // Can't be more specific as we get different exceptions locally vs in the build pipeline
            var expectedMsg = "Internal SDK error occurred when calling ListConfigurationSets";

            Assert.That(
                () => api.ListConfigurationSetsWithHttpInfo("Personal"),
                Throws.InstanceOf<ApiException>()
                    .With.Message.Contains(expectedMsg));

            // Note: these non-"WithHttpInfo" methods just unwrap the `Data` property from the call above.
            // But these were the problematic ones, as they would previously just return a null value in this scenario.
            Assert.That(
                () => api.ListConfigurationSets("Personal"),
                Throws.InstanceOf<ApiException>()
                    .With.Message.Contains(expectedMsg));
        }

        [Test]
        public void Invalid_Requested_Api_Throws()
        {
            Assert.That(() => _factory.Api<InvalidApi>(), Throws.TypeOf<InvalidOperationException>());
        }

        class InvalidApi : IApiAccessor
        {
            public IReadableConfiguration Configuration { get; set; }
            public string GetBasePath()
            {
                throw new NotImplementedException();
            }

            public ExceptionFactory ExceptionFactory { get; set; }
        }
    }
}

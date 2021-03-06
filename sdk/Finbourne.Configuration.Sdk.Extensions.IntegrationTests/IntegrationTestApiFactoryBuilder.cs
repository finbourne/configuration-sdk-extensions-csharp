using System.IO;

namespace Finbourne.Configuration.Sdk.Extensions.IntegrationTests
{
    public  class IntegrationTestApiFactoryBuilder
    {
        public static IApiFactory CreateApiFactory(string secretsFile)
        {
            return File.Exists(secretsFile)
                ? ApiFactoryBuilder.Build(secretsFile)
                : ApiFactoryBuilder.Build(null);
        }

        public static ApiConfiguration CreateApiConfiguration(string secretsFile)
        {
            return File.Exists(secretsFile)
                ? ApiConfigurationBuilder.Build(secretsFile)
                : ApiConfigurationBuilder.Build(null);
        }
    }
}

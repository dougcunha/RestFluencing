namespace RestFluencing.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RestFluencing.Client.HttpApiClient;

    [TestClass]
    public class HttpApiClientBuilderTests
    {
        [TestMethod]
        public void HttpClientBuilderCreatesHttpApiClient()
        {
            var builder = new HttpApiClientBuilder();

            var client = builder.Create();

            Assert.IsInstanceOfType(client, typeof(HttpApiClient));
        }

        [TestMethod]
        public void WhenNoClientShouldSetTheHttpApiClientBuilder()
        {
            var config = new RestConfiguration();
            config.UsingWebApiClient();

            Assert.IsInstanceOfType(config.ClientFactory, typeof(HttpApiClientBuilder));
        }
    }
}
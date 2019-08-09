namespace RestFluencing.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RestFluencing.Assertion;
    using RestFluencing.Tests.Clients;
    using RestFluencing.Tests.Models;

    [TestClass]
    public class InlineAssertionRuleTests
    {
        private RestConfiguration _configuration;
        private TestApiFactory _factory;

        [TestInitialize]
        public void Setup()
        {
            _configuration = RestConfigurationHelper.Default();
            _factory = _configuration.ClientFactory as TestApiFactory;
        }

        [TestMethod]
        public void WhenBlankShouldFailAssertion()
        {
            Rest.Get("/product/apple", _configuration)
                .Response()
                .Execute()
                .Assert();
        }

        [TestMethod]
        public void WhenInlineFunctionReturnsFalseShouldFailAssertion()
        {
            Rest.Get("/product/apple", _configuration)
                .Response()
                .ReturnsModel<Product>
                (
                    lambda: product => product.Name == "Hello",
                    "My custom error message"
                )
                .Execute()
                .ShouldFail();
        }

        [TestMethod]
        public void WhenInlineFunctionReturnsTrueeShouldPassAssertion()
        {
            Rest.Get("/product/apple", _configuration)
                .Response()
                .ReturnsModel<Product>
                (
                    lambda: product => product.Name == "Apple",
                    "My custom error message"
                )
                .Execute()
                .ShouldPass();
        }
    }
}
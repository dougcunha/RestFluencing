// ReSharper disable TestClassNameDoesNotMatchFileNameWarning
namespace RestFluencing.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RestFluencing.JsonSchema;
    using RestFluencing.Tests.Clients;
    using RestFluencing.Tests.Models;

    [TestClass]
    public class HasJsonSchemaRuleTests
    {
        private RestConfiguration _configuration;
        private TestApiFactory _factory;

        [TestMethod]
        public void FailOnDifferentModelNotArray()
        {
            Rest.Get("/product", _configuration)
                .Response()
                .HasJsonSchema<Product>()
                .Execute()
                .ShouldFailForRule<JsonModelSchemaAssertionRule<Product>>();
        }

        [TestMethod]
        public void FailOnManyItemsButExpectingDifferentModel()
        {
            Rest.Get("/promo", _configuration)
                .Response()
                .HasJsonSchema<IList<Product>>()
                .Execute()
                .ShouldFailForRule<JsonModelSchemaAssertionRule<IList<Product>>>();
        }

        [TestMethod]
        public void FailOnSingleItemExpectingList()
        {
            Rest.Get("/product/apple", _configuration)
                .Response()
                .HasJsonSchema<IList<Product>>()
                .Execute()
                .ShouldFailForRule<JsonModelSchemaAssertionRule<IList<Product>>>();
        }

        [TestInitialize]
        public void Setup()
        {
            _configuration = RestConfigurationHelper.Default();
            _factory = _configuration.ClientFactory as TestApiFactory;
        }

        [TestMethod]
        public void SuccessEmptyListModel()
        {
            Rest.Get("/product/empty", _configuration)
                .Response()
                .HasJsonSchema<IList<Product>>()
                .Execute()
                .ShouldPass();
        }

        [TestMethod]
        public void SuccessListModel()
        {
            Rest.Get("/product", _configuration)
                .Response()
                .HasJsonSchema<IList<Product>>()
                .Execute()
                .ShouldPass();
        }

        [TestMethod]
        public void SuccessProductModel()
        {
            Rest.Get("/product/apple", _configuration)
                .Response()
                .HasJsonSchema<Product>()
                .Execute()
                .ShouldPass();
        }

        [TestMethod]
        public void SuccessPromoModel()
        {
            Rest.Get("/promo/apple", _configuration)
                .Response()
                .HasJsonSchema<Promo>()
                .Execute()
                .ShouldPass();
        }
    }
}
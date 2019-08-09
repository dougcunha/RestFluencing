namespace RestFluencing.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AfterEventsOnRequestTests
    {
        [TestMethod]
        public void AfterRequest_HasResponseAvailable()
        {
            string contentFromConfig = null;
            string contentFromRequest = null;

            // Arrange
            var config = RestConfigurationHelper.Default()
                .AfterRequest
                (
                    context => contentFromConfig = context.Response.Content
                );

            var request = config.Get("/product/apple")
                .AfterRequest
                (
                    context => contentFromRequest = context.Response.Content
                );

            // Act
            request.Response().Assert();

            // Assert
            Assert.IsNotNull(contentFromConfig);
            Assert.IsNotNull(contentFromRequest);

            Assert.IsTrue(contentFromConfig.Contains("Apple"));
            Assert.IsTrue(contentFromRequest.Contains("Apple"));
        }

        [TestMethod]
        public void AfterRequest_WhenAddedOnConfiguration_ShouldCallDelegate()
        {
            // Arrange
            var callFromConfig = false;

            var config = RestConfigurationHelper.Default()
                .AfterRequest
                (
                    context => callFromConfig = true
                );

            // Act
            config.Get("/null")
                .Response()
                .Assert();

            // Assert
            Assert.IsTrue(callFromConfig);
        }

        [TestMethod]
        public void AfterRequest_WhenAddedOnRestRequest_ShouldCallDelegate()
        {
            // Arrange
            var callFromRequest = false;
            var config = RestConfigurationHelper.Default();

            var request = config.Get("/null")
                .AfterRequest
                (
                    context => callFromRequest = true
                );

            // Act
            request.Response().Assert();

            // Assert
            Assert.IsTrue(callFromRequest);
        }

        [TestMethod]
        public void AfterRequest_WhenAddedToConfigThenRequest_ShouldCallEachDelegate()
        {
            // Arrange
            var firstCallFromConfig = false;
            var secondCallFromRequest = false;

            var config = RestConfigurationHelper.Default()
                .AfterRequest
                (
                    context => firstCallFromConfig = true
                );

            var request = config.Get("/null")
                .AfterRequest
                (
                    context => secondCallFromRequest = true
                );

            // Act
            request.Response().Assert();

            // Assert
            Assert.IsTrue(firstCallFromConfig);
            Assert.IsTrue(secondCallFromRequest);
        }

        [TestMethod]
        public void AfterRequest_WhenMultipleDelegates_ShouldCallEachDelegate()
        {
            // Arrange
            var firstCallFromRequest = false;
            var secondCallFromRequest = false;
            var config = RestConfigurationHelper.Default();

            var request = config.Get("/null")
                .AfterRequest
                (
                    context => firstCallFromRequest = true
                )
                .AfterRequest
                (
                    context => secondCallFromRequest = true
                );

            // Act
            request.Response().Assert();

            // Assert
            Assert.IsTrue(firstCallFromRequest);
            Assert.IsTrue(secondCallFromRequest);
        }

        [TestMethod]
        public void WhenThereIsNoEventRegisteredDoesntBreak()
        {
            // Arrange
            var config = RestConfigurationHelper.Default();

            // Act
            config.Get("/null")
                .Response()
                .Assert();
        }
    }
}
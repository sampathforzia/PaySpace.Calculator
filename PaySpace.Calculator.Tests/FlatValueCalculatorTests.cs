using NUnit.Framework;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class FlatValueCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(199999, 9999.95)]
        [TestCase(100, 5)]
        [TestCase(200000, 10000)]
        [TestCase(6000000, 10000)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange
            var c = new PaySpace.Calculator.Web.Services.CalculatorHttpService();
            var result = await c.CalculateTaxAsync(new Web.Services.Models.CalculateRequest() { PostalCode = "A100", Income = income });
            Assert.IsTrue(result.Tax.Equals(expectedTax));
            // Assert
        }
    }
}
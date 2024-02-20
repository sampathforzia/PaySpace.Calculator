using NUnit.Framework;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class ProgressiveCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(-1, 0)]
        [TestCase(50, 5)]
        [TestCase(8350.1, 835.01)]
        [TestCase(8351, 1252.65)]
        [TestCase(33951, 8487.75)]
        [TestCase(82251, 23030.28)]
        [TestCase(171550, 48034)]
        [TestCase(999999, 349999.65)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange
            var c = new PaySpace.Calculator.Web.Services.CalculatorHttpService();
          var result = await  c.CalculateTaxAsync(new Web.Services.Models.CalculateRequest() { PostalCode = "7441", Income = income });
            // Act
            Assert.IsTrue(result.Tax.Equals(expectedTax));
            // Assert
        }
    }
}
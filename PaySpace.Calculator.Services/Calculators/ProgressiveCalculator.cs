using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class ProgressiveCalculator : ICalculator
    {
        public RateType Type { get; set; }

        public decimal Rate { get; set; }

        public ProgressiveCalculator(RateType type, decimal rate) { 
         Type= type;
            Rate= rate;
        }
        public ProgressiveCalculator()
        {
            Type =  RateType.Percentage;
            Rate = 0;
        }
        public CalculateResult Calculate(decimal income)
        {
            decimal dc = 0;
            if (Type == RateType.Amount)
            {
                dc = income * Rate;
            }
            else if (Type == RateType.Percentage)
            {
                dc = income * (Rate / 100);
            }
            return (new CalculateResult() { Calculator = CalculatorType.Progressive, Tax = dc });
        }
    }
}
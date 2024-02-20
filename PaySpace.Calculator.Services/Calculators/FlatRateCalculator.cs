using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class FlatRateCalculator : ICalculator
    {
        public RateType Type { get; set; }

        public decimal Rate { get; set; }
        public FlatRateCalculator()
        {
            Type = RateType.Percentage;
            Rate = 0;
        }
        public FlatRateCalculator(RateType type, decimal rate)
        {
            Type = type;
            Rate = rate;
        }
        public CalculateResult Calculate(decimal income)
        {
            decimal dc = 0;
            if (Type == RateType.Amount)
            {
                dc = income * Rate;
            }
            else if (Type == RateType.Percentage) { 
            dc = income * (Rate/100);
            }
            return  (new CalculateResult() { Calculator =  CalculatorType.FlatRate, Tax = dc });
        }

    
    }
}
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class FlatRateCalculator : ICalculator
    {
        
        private readonly ICalculatorSettingsService _calculatorSettingsService = null;
        public FlatRateCalculator(ICalculatorSettingsService calculatorSettingsService)
        {
            _calculatorSettingsService = calculatorSettingsService;
        }
        public FlatRateCalculator()
        {
           
        }
        
        public CalculateResult Calculate(decimal income)
        {
            var _calSettings = _calculatorSettingsService.GetSettingsAsync(CalculatorType.FlatRate);
            var _setting = _calSettings.Result.FirstOrDefault();
            if (_setting != null)
            {
                decimal totalTax = 0;
                totalTax =  (income * (_setting.Rate / 100));
                return (new CalculateResult() { Calculator = CalculatorType.FlatRate, Tax = totalTax });
            }
            else {
                return (new CalculateResult() { Calculator = CalculatorType.FlatRate, Tax = 0 });
            }
          
        }

    
    }
}
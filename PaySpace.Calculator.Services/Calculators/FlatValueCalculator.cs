using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class FlatValueCalculator : ICalculator
    {
        private readonly ICalculatorSettingsService _calculatorSettingsService = null;
        public FlatValueCalculator( )
        {
          
        }     
        public FlatValueCalculator(ICalculatorSettingsService calculatorSettingsService)
        {
            _calculatorSettingsService = calculatorSettingsService;
        }
        public CalculateResult Calculate(decimal income)
        {
            decimal totalTax = 0;
            var _calSettings = _calculatorSettingsService.GetSettingsAsync(CalculatorType.FlatValue);
            var _setting = _calSettings.Result.Where(x => x.From <= income && (x.To.HasValue == true ? x.To : 0) >= income).FirstOrDefault();

            if (_setting != null)
            {
                totalTax = income * (_setting.Rate / 100);
            }

            _setting = _calSettings.Result.Where(x => x.From <= income && (x.To.HasValue == true ? x.To : 0) == 0).FirstOrDefault();
            if (_setting != null)
            {
                totalTax =_setting.Rate;
            }

            return (new CalculateResult() { Calculator = CalculatorType.FlatValue, Tax = totalTax });
        }
    }
}
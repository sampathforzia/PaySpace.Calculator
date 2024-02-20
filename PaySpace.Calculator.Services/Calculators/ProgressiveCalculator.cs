using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class ProgressiveCalculator : ICalculator
    {         
        private readonly ICalculatorSettingsService _calculatorSettingsService=null;
        public ProgressiveCalculator(ICalculatorSettingsService calculatorSettingsService)
        {
            _calculatorSettingsService = calculatorSettingsService;
        }

        public ProgressiveCalculator() { 
         
         
        }      
        public CalculateResult Calculate(decimal income)
        {
            var _calSettings = _calculatorSettingsService.GetSettingsAsync( CalculatorType.Progressive);
            decimal roundIncome =  (income);
            var _settings = _calSettings.Result.Where(x => ((x.From <= roundIncome))).ToList();
            decimal dc = 0;
            decimal calIncome = income;
            int index = 0;
            foreach (var settings in _settings)
            {
                var _setting = settings;
                if (index == _settings.Count - 1)
                {
                    calIncome = (decimal)(income - settings.From);
                    if (_setting != null)
                    {
                        if (_setting.RateType == RateType.Amount)
                        {
                            dc = dc + (calIncome * _setting.Rate);
                        }
                        else if (_setting.RateType == RateType.Percentage)
                        {
                            dc = dc + (calIncome * (_setting.Rate / 100));
                        }                                               
                    }
                }
                else
                {
                    calIncome = (decimal)(settings.To == null ? 0 : settings.To) - settings.From;
                    if (_setting != null)
                    {
                        if (_setting.RateType == RateType.Amount)
                        {
                            dc = dc + (calIncome * _setting.Rate);
                        }
                        else if (_setting.RateType == RateType.Percentage)
                        {
                            dc = dc + (calIncome * (_setting.Rate / 100));
                        }
                    }
                }
                index = index + 1;
            }                      
          
            return (new CalculateResult() { Calculator = CalculatorType.Progressive, Tax = dc });
        }
    }
}
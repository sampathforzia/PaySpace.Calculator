using Azure.Core;
using Corex.Model.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PaySpace.Calculator.Services
{
    /// <summary>
    /// This service class handle tax calcuation for various type of calcuator    /// 
    /// </summary>
    public class TaxCalculatorService: ITaxCalculatorService
    {
        public readonly IPostalCodeService _postalCodeService;
        public readonly ICalculatorSettingsService _calculatorSettingService;
        public readonly IHistoryService _historyService;
        public TaxCalculatorService(IPostalCodeService postalCodeService , ICalculatorSettingsService calculatorSettingsService, IHistoryService historyService = null)
        {

            _postalCodeService = postalCodeService;
            _calculatorSettingService = calculatorSettingsService;
            _historyService = historyService;
        }
        /// <summary>
        /// This method perform the tax calculation based on the postal code and it range of income.
        /// </summary>
        /// <param name="postalCode"></param>
        /// <param name="income"></param>
        /// <returns>return caculation result which is contact tax and type of caculator</returns>
        /// <exception cref="CalculatorException"></exception>
        public async Task<CalculateResult> TaxCalcuation(string? postalCode, decimal income) {
            decimal roundIncome = Math.Floor(income);
            if (postalCode == null) {
                throw new CalculatorException("Invalid postal code");
            }
            var _postal =await _postalCodeService.CalculatorTypeAsync(postalCode);
            if(_postal == null )
            {
                throw new CalculatorException("Invalid postal code");
            }          
            var _calSettings = _calculatorSettingService.GetSettingsAsync(_postal.Value);
            if (_calSettings.IsFaulted==false)
            {
                var _setting = _calSettings.Result.Where(x => ((x.From <= roundIncome && x.To >= roundIncome) 
                || (x.From <= roundIncome && x.To==0)) ).FirstOrDefault();
                if (_setting != null)
                {
                    var result= Calculate(_setting, _postal.Value, income);
                    if (result.Calculator != CalculatorType.None) {
                        await _historyService.AddAsync(new CalculatorHistory
                        {
                            Tax = result.Tax,
                            Calculator = result.Calculator,
                            PostalCode = postalCode,
                            Income = income,
                        });
                    }
                    return result;
                }
                else {
                    throw new CalculatorException("Unable to find calculator");
                }                   
            }
            else
            {
                throw new CalculatorException("Unable to find calculator");
            }            
        }
        /// <summary>
        /// It is the private method which support to switch the calculator based on calcuation type
        /// </summary>
        /// <param name="_setting"></param>
        /// <param name="type"></param>
        /// <param name="income"></param>
        /// <returns></returns>
        private CalculateResult Calculate(CalculatorSetting _setting, CalculatorType type, decimal income) {
            CalculateResult result;                    
                    ICalculator cal;
                    if (type == CalculatorType.Progressive)
                    {
                        cal = new ProgressiveCalculator(_setting.RateType, _setting.Rate);
                    }
                    else if (type == CalculatorType.FlatRate)
                    {
                        cal = new FlatRateCalculator(_setting.RateType, _setting.Rate);
                    }
                    else if (type == CalculatorType.FlatValue)
                    {
                        cal = new FlatValueCalculator(_setting.RateType, _setting.Rate);
                    }
                    else
                    {
                        cal = new UnknowCalculator();
                    }
           
            result = cal.Calculate(income);
                    return result;
                
        }

       
    }
}

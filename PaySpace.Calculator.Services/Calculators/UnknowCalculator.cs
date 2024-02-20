using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculator.Services.Calculators
{
    internal class UnknowCalculator : ICalculator

    {
        public RateType Type { get; set; }

        public decimal Rate{get;set;}
       
        public UnknowCalculator()
        {
             
        }
        public CalculateResult Calculate(decimal income)
        {
            return new CalculateResult() { Calculator = CalculatorType.None, Tax = 0 };
        }
    }
}

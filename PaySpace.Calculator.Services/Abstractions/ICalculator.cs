using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculator.Services.Abstractions
{
    internal interface ICalculator
    {
        public RateType Type { get; }
        public decimal Rate { get; }
        CalculateResult Calculate(decimal income);         
    }
}

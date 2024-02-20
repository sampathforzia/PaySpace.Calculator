
using PaySpace.Calculator.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface ITaxCalculatorService
    {
          Task<CalculateResult> TaxCalcuation(string? postalCode, decimal income);
    }
}

﻿using MapsterMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;
using System.Drawing.Imaging;

namespace PaySpace.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    
    public sealed class CalculatorController(
        ILogger<CalculatorController> logger,
        IHistoryService historyService,
        IPostalCodeService postalCodeService,
        IMapper mapper)
        : ControllerBase
    {
        [HttpPost("calculate-tax")]

        public async Task<ActionResult<CalculateResult>> Calculate(CalculateRequest request)
        {
            try
            {

                var context =  HttpContext.RequestServices.GetService<ITaxCalculatorService>();
                if (context != null)
                {
                    var result = await context.TaxCalcuation(request.PostalCode, request.Income);
                    return this.Ok(result);
                }
                else {                    
                    return this.BadRequest("Unable to find calculator");
                }                 
              
            }
            catch (CalculatorException e)
            {
                logger.LogError(e, e.Message);
                return this.BadRequest(e.Message);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("history")]
        public async Task<ActionResult<List<CalculatorHistory>>> History()
        {
            var history = await historyService.GetHistoryAsync();

            return this.Ok(mapper.Map<List<CalculatorHistoryDto>>(history));
        }

        [HttpGet("postalcodes")]
        public async Task<ActionResult<List<CalculatorHistory>>> Postal()
        {
            var history = await postalCodeService.GetPostalCodesAsync();

            return this.Ok(mapper.Map<List<PostalCodeDto>>(history));
        }
    }
}
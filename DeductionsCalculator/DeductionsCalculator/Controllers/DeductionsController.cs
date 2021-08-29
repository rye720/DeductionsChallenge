using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DeductionsCalculatorApi
{
    [Route("[controller]")]
    public class DeductionsController : Controller
    {
        private readonly IDeductionsService _deductionsService;

        public DeductionsController(IDeductionsService deductionsService)
        {
            _deductionsService = deductionsService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetBenefitsPackageByGuidAsync([FromQuery] Guid Id)
        {
            var result = await _deductionsService.GetByIdAsync(Id);
            return Ok(result);
        }
    }
}


using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

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

        [HttpGet("BenefitsPackage/{Id}")]
        public async Task<IActionResult> GetBenefitsPackageByGuidAsync(Guid Id)
        {
            var result = await _deductionsService.GetBenefitsPackageByIdAsync(Id);
            return Ok(result);
        }

        [HttpGet("Employee/{Id}")]
        public async Task<IActionResult> GetEmployeeWithDependentsAsync(Guid Id)
        {
            var result = await _deductionsService.GetEmployeeWithDependentsByEmployeeIdAsync(Id);
            return Ok(result);
        }

        [HttpPost("EmployeeCostPreview")]
        public async Task<IActionResult> CreateEmployeeAndGetCostPreviewAsync([FromBody] Employee employee, [FromQuery] Guid? benefitsPackageId = null)
        {
            var result = await _deductionsService.CreateNewEmployeeAndGetCostPreviewAsync(employee, benefitsPackageId);
            return Ok(result);
        }

        [HttpPost("Employee")]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] Employee employee)
        {
            var result = await _deductionsService.CreateEmployeeAsync(employee);
            return Ok(result);
        }
    }
}


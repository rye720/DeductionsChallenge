using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Microsoft.Extensions.Options;
using Models.Common;
using Models.Models;
using Models.ResourceModels;

namespace Services.Services
{
    public class DeductionsService : IDeductionsService
    {
        private readonly ApplicationSettings _appSettings;

        private readonly IBenefitsPackageRepository _benefitsPackageRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DeductionsService(
            IBenefitsPackageRepository benefitsPackageRepository,
            IEmployeeRepository employeeRepository,
            IOptions<ApplicationSettings> appSettings
            )
        {
            _benefitsPackageRepository = benefitsPackageRepository;
            _employeeRepository = employeeRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<BenefitsPackage> GetBenefitsPackageByIdAsync(Guid Id)
        {
            return await _benefitsPackageRepository.GetByIdAsync(Id);
        }

        public async Task<Employee> GetEmployeeWithDependentsByEmployeeIdAsync(Guid Id)
        {
            return await _employeeRepository.GetEmployeeWithDependentsByEmployeeIdAsync(Id);
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            return await _employeeRepository.AddAsync(employee);
        }

        public async Task<DeductionsPreviewResourceModel> GetDeductionsPreviewFromNewEmployeeAsync(Employee employee, Guid? benefitsPackageId)
        {
            var newEmployee = await _employeeRepository.AddAsync(employee);

            // need to do this better!
            var newEmployeeWithDependents = await _employeeRepository.GetEmployeeWithDependentsByEmployeeIdAsync(newEmployee.Id);

            var benefitsPackage = benefitsPackageId != null && benefitsPackageId.Value != Guid.Empty ?
                await _benefitsPackageRepository.GetByIdAsync(benefitsPackageId.Value) :
                await _benefitsPackageRepository.GetDefaultBenefitsPackageAsync();

            var employeeBenefitsCost = benefitsPackage.YearlyEmployeeCost;

            // apply benefits discount to employee if applicable
            if (!string.IsNullOrEmpty(newEmployeeWithDependents.Name) && benefitsPackage.DiscountInitialPercentage != null)
            {
                if (newEmployeeWithDependents.Name.ToLowerInvariant().StartsWith(benefitsPackage.DiscountInitial.ToString().ToLowerInvariant()))
                {
                    employeeBenefitsCost -= employeeBenefitsCost * benefitsPackage.DiscountInitialPercentage.Value;
                }
            }

            // calc employee salary
            var employeeSalaryCost = _appSettings.PaySettings.PaycheckAmount * _appSettings.PaySettings.PaychecksPerYear;

            // calc cost of dependents
            decimal dependentsCost = newEmployeeWithDependents.Dependents.Count * benefitsPackage.YearlyDependentCost;
            foreach (var dependent in newEmployeeWithDependents.Dependents)
            {
                if (!string.IsNullOrEmpty(dependent.Name) && benefitsPackage.DiscountInitialPercentage != null)
                {
                    if (dependent.Name.ToLowerInvariant().StartsWith(benefitsPackage.DiscountInitial.ToString().ToLowerInvariant()))
                    {
                        dependentsCost -= benefitsPackage.YearlyDependentCost * benefitsPackage.DiscountInitialPercentage.Value;
                    }
                }
            }

            return new DeductionsPreviewResourceModel(newEmployee, employeeSalaryCost + employeeBenefitsCost, dependentsCost);
        }

        public async Task<DeductionsPreviewResourceModel> GetDeductionsPreviewByEmployeeIdAsync(Guid Id)
        {
            return new DeductionsPreviewResourceModel();
        }

    }
}

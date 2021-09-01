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

        #region PublicMethods
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

        public async Task<DeductionsPreviewResourceModel> CreateNewEmployeeAndGetCostPreviewAsync(Employee employee, Guid? benefitsPackageId)
        {
            var newEmployee = await _employeeRepository.AddAsync(employee);
            // need to do this better!
            var newEmployeeWithDependents = await _employeeRepository.GetEmployeeWithDependentsByEmployeeIdAsync(newEmployee.Id);

            var benefitsPackage = benefitsPackageId != null && benefitsPackageId.Value != Guid.Empty ?
                await _benefitsPackageRepository.GetByIdAsync(benefitsPackageId.Value) :
                await _benefitsPackageRepository.GetDefaultBenefitsPackageAsync();

            // calc employee cost
            var employeeCost = GetEmployeeCost(newEmployeeWithDependents, benefitsPackage);

            // calc cost of dependents
            var dependentsCost = GetDependentsCost(newEmployeeWithDependents.Dependents, benefitsPackage);

            return new DeductionsPreviewResourceModel(newEmployeeWithDependents, employeeCost, dependentsCost);
        }

        public async Task<DeductionsPreviewResourceModel> GetDeductionsPreviewByEmployeeIdAsync(Guid Id)
        {
            // add if time allows, could be helpful
            return new DeductionsPreviewResourceModel();
        }

        #endregion PublicMethods


        
        #region PrivateMethods
        private decimal GetEmployeeCost(Employee employee, BenefitsPackage benefitsPackage)
        {
            var employeeSalaryCost = _appSettings.PaySettings.PaycheckAmount * _appSettings.PaySettings.PaychecksPerYear;
            var employeeBenefitsCost = benefitsPackage.YearlyEmployeeCost;

            if(IsEligibleForDiscount(employee.Name, benefitsPackage.DiscountInitial))
            {
                employeeBenefitsCost -= benefitsPackage.YearlyEmployeeCost * benefitsPackage.DiscountInitialPercentage.Value;
            }

            return employeeSalaryCost + employeeBenefitsCost;
        }

        private decimal GetDependentsCost(IList<Dependent> dependents, BenefitsPackage benefitsPackage)
        {
            var dependentsCost = dependents.Count * benefitsPackage.YearlyDependentCost;

            foreach (var dependent in dependents)
            {
                if (IsEligibleForDiscount(dependent.Name, benefitsPackage.DiscountInitial))
                {
                    dependentsCost -= benefitsPackage.YearlyDependentCost * benefitsPackage.DiscountInitialPercentage.Value;
                }
            }

            return dependentsCost;
        }

        private bool IsEligibleForDiscount(string name, char initial)
        {
            if(!string.IsNullOrEmpty(name))
            {
                return name.ToLowerInvariant().StartsWith(initial.ToString().ToLowerInvariant());
            }

            return false;
        }

        #endregion
    }
}

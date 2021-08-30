using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Models.Models;

namespace Services.Services
{
    public class DeductionsService : IDeductionsService
    {
        private readonly IBenefitsPackageRepository _benefitsPackageRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DeductionsService(
            IBenefitsPackageRepository benefitsPackageRepository,
            IEmployeeRepository employeeRepository
            )
        {
            _benefitsPackageRepository = benefitsPackageRepository;
            _employeeRepository = employeeRepository;
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
    }
}

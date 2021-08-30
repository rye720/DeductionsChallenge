using System;
using System.Threading.Tasks;
using Models.Models;

public interface IDeductionsService
{
    public Task<BenefitsPackage> GetBenefitsPackageByIdAsync(Guid Id);
    public Task<Employee> GetEmployeeWithDependentsByEmployeeIdAsync(Guid Id);
    Task<Employee> CreateEmployeeAsync(Employee employee);
}

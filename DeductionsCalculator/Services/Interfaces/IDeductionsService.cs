using System;
using System.Threading.Tasks;
using Models.Models;
using Models.ResourceModels;

public interface IDeductionsService
{
    public Task<BenefitsPackage> GetBenefitsPackageByIdAsync(Guid Id);
    public Task<Employee> GetEmployeeWithDependentsByEmployeeIdAsync(Guid Id);
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task<DeductionsPreviewResourceModel> CreateNewEmployeeAndGetCostPreviewAsync(Employee employee, Guid? benefitsPackageId);
}

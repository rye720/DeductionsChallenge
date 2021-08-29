using System;
using System.Threading.Tasks;
using Models.Models;

public interface IDeductionsService
{
    public Task<BenefitsPackage> GetByIdAsync(Guid Id);
}

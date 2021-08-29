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

        public DeductionsService(
            IBenefitsPackageRepository benefitsPackageRepository
            )
        {
            _benefitsPackageRepository = benefitsPackageRepository;
        }

        public async Task<BenefitsPackage> GetByIdAsync(Guid Id)
        {
            return await _benefitsPackageRepository.GetByIdAsync(Id);
        }
    }
}

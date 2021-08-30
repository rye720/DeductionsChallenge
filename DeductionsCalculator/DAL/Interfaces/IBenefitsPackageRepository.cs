using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DAL.Interfaces
{
    public interface IBenefitsPackageRepository : IRepository<BenefitsPackage>
    {
        Task<BenefitsPackage> GetDefaultBenefitsPackageAsync();
    }
}

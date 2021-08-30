using System;
using System.Data;
using System.Threading.Tasks;
using DAL.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Models.Models;

namespace DAL.Repositories
{
    public class BenefitsPackageRepository : BaseRepository, IBenefitsPackageRepository
    {

        public BenefitsPackageRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<BenefitsPackage> GetByIdAsync(Guid Id)
        {
            var query = @"SELECT bp.Id,
                        bp.YearlyEmployeeCost,
                        bp.YearlyDependentCost,
                        bp.DiscountInitial,
                        bp.DiscountInitialPercentage
                        FROM BenefitsPackage bp
                        WHERE bp.Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", Id, DbType.Guid);

            using (var connection = CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<BenefitsPackage>(query, parameters);
                return result;
            }
        }

        public Task<BenefitsPackage> AddAsync(BenefitsPackage entity)
        {
            throw new NotImplementedException();
        }

        public Task<BenefitsPackage> UpdateAsync(BenefitsPackage entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}

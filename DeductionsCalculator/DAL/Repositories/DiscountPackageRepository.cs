using Interfaces.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class DiscountPackageRepository : IDiscountPackageRepository
    {
        private readonly DapperContext _context;
        public DiscountPackageRepository(DapperContext context)
        {
            _context = context;
        }
    }
}

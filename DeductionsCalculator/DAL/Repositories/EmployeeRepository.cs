using Interfaces.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;
        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }
    }
}

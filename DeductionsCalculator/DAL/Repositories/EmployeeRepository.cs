using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Models.Models;

namespace DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        public EmployeeRepository()
        {
         
        }

        public Task<Employee> AddAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateAsync(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}

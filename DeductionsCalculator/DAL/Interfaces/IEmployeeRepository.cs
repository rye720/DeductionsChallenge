using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DAL.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> GetEmployeeWithDependentsByEmployeeIdAsync(Guid Id);
    }
}

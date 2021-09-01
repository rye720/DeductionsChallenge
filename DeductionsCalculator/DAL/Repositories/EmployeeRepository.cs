using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Models.Models;

namespace DAL.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {

        public EmployeeRepository(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<Employee> GetByIdAsync(Guid Id)
        {
            var query = @"SELECT 
                        e.Id,
                        e.Name
                        FROM Employee e
                        WHERE e.Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", Id, DbType.Guid);

            using (var connection = CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<Employee>(query, parameters);
                return result;
            }
        }

        public async Task<Employee> GetEmployeeWithDependentsByEmployeeIdAsync(Guid Id)
        {
            var query = @"SELECT e.Id, e.Name, e.IsActive, e.CreatedAt, e.UpdatedAt,
                        d.Id, d.Name, d.EmployeeId, d.IsActive, d.CreatedAt, d.UpdatedAt
                        FROM Employee e
                        LEFT JOIN Dependent d ON e.Id = d.EmployeeId
                        WHERE e.Id = @Id;";

            var parameters = new DynamicParameters();
            parameters.Add("Id", Id, DbType.Guid);

            using (var connection = CreateConnection())
            {
                var employeeDict = new Dictionary<Guid, Employee>();
                var employees = await connection.QueryAsync<Employee, Dependent, Employee>(
                    query, (employee, dependent) =>
                    {
                        if (!employeeDict.TryGetValue(employee.Id, out var currentEmployee))
                        {
                            currentEmployee = employee;
                            employeeDict.Add(currentEmployee.Id, currentEmployee);
                        }
                        if(dependent != null)
                        {
                            currentEmployee.Dependents.Add(dependent);
                        }
                        return currentEmployee;
                    }, param: parameters
                );

                return employees.FirstOrDefault();
            }
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            // OUTPUT INSERTED.* needed for full return of inserted Employee
            var insertEmployeeQuery = "INSERT INTO Employee (Name) OUTPUT INSERTED.* VALUES (@Name)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name, DbType.String);

            //add using TransactionScope
            using (var connection = CreateConnection())
            {
                var newEmployee = await connection.QuerySingleAsync<Employee>(insertEmployeeQuery, parameters);

                if (entity.Dependents != null && entity.Dependents.Any())
                {
                    foreach (var dependent in entity.Dependents)
                    {
                        dependent.EmployeeId = newEmployee.Id;
                    }
                    // easy way to get inserted objects returned here? (without querying again)
                    var insertDependentsQuery = "INSERT INTO Dependent (Name, EmployeeId) VALUES (@Name, @employeeId)";
                    await connection.ExecuteAsync(insertDependentsQuery, entity.Dependents.ToList());
                }

                return newEmployee;
            }
        }

        public Task<bool> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateAsync(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}

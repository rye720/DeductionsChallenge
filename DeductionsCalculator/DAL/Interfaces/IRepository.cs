using System;
using System.Threading.Tasks;
using Models.Models;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(Guid Id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(Guid Id);
    }
}

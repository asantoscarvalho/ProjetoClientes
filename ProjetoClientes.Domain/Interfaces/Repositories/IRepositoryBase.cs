using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoClientes.Domain.Interfaces.Repositories
{
 
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        Task<TEntity> GetById(int id);
        Task<TEntity[]> GetAll();
        void Update(TEntity obj);
        void Delete(TEntity obj);
        void DeleteRange<TEntity>(TEntity[] entityArray);
        Task<bool> SaveChangesAsync();

        void Dispose();
    }
    
}
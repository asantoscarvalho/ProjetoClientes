using System;
using System.Threading.Tasks;
using ProjetoClientes.Infra.Context;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjetoClientes.Domain.Interfaces.Repositories;
using ProjetoClientes.Domain.Entities;

namespace ProjetoClientes.Infra.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected ProjetoClientesContext _context ;

        public RepositoryBase(ProjetoClientesContext context)
        {
            this._context = context;
        }

        public void Add(TEntity obj)
        {
            _context.Add(obj);
        }

        public void Delete(TEntity obj)
        {
            _context.Remove(obj);
        }

        public void DeleteRange<TEntity>(TEntity[] entityArray)
        {
            _context.RemoveRange(entityArray);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public virtual async Task<TEntity[]> GetAll()
        {
            return await _context.Set<TEntity>().ToArrayAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
             return ( await _context.SaveChangesAsync()) > 0;
        }

        public void Update(TEntity obj)
        {
             _context.Update(obj);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using ProjetoClientes.Domain.Entities;
using ProjetoClientes.Domain.Interfaces.Repositories;
using ProjetoClientes.Infra.Context;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjetoClientes.Infra.Repositories
{
    public class ClienteRepository  : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(ProjetoClientesContext context) : base(context) {}


      public override async Task<Cliente[]> GetAll()
      {
            IQueryable<Cliente> query = _context.Cliente.Include(end => end.enderecos);

            return await query.ToArrayAsync();

      }

      public override async Task<Cliente> GetById(int id)
      {
            Cliente query = await _context.Cliente.Include(end => end.enderecos).Where(x => x.Id == id).FirstOrDefaultAsync();

            return query;

      }

    }
}
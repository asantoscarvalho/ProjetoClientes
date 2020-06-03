using Microsoft.EntityFrameworkCore;
using ProjetoClientes.Domain.Entities;
using ProjetoClientes.Domain.Interfaces.Repositories;
using ProjetoClientes.Infra.Context;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoClientes.Infra.Repositories
{
    public class EnderecoRepository: RepositoryBase<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(ProjetoClientesContext context) : base(context) {}


        public override async Task<Endereco[]> GetAll()
        {
            IQueryable<Endereco> query = _context.Endereco.Include(end => end.Cliente);

            return await query.ToArrayAsync();

        }

        public override async Task<Endereco> GetById(int id)
        {
            Endereco query = await _context.Endereco.Include(cli => cli.Cliente).Where(x => x.Id == id).FirstOrDefaultAsync();

            return query;

        }


    }
}
using Microsoft.EntityFrameworkCore;
using ProjetoClientes.Domain.Entities;
using ProjetoClientes.Infra.Mapping;
using System.Linq;

namespace ProjetoClientes.Infra.Context
{
    public class ProjetoClientesContext : DbContext
    {
       public ProjetoClientesContext(DbContextOptions<ProjetoClientesContext> options) : base (options) {}

       public DbSet<Cliente> Cliente {get; set;}
       public DbSet<Endereco> Endereco {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());

          
          //  modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjetoClientesContext).Assembly);

            

            base.OnModelCreating(modelBuilder);

        }

    }
}
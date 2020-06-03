
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoClientes.Infra.Context;


namespace ProjetoClientes.Api.Configuration
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ProjetoClientesContext>(
                x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))

             );


            return services;
        }

    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace ProjetoClientes.Api.Configuration
{
    public static class IncludeSwaggerConfig
    {

        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Projeto Clientes",
                    Description = "Projeto de Cadastro de clientes",
                    Contact = new OpenApiContact
                    {
                        Name = "André Luiz Santos de Carvalho",
                        Email = "andreluizscarvalho@gmail.com",
                        Url = new Uri("https://github.com/asantoscarvalho/ProjetoClientes"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

    }
}

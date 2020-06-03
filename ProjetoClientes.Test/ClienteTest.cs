using ProjetoClientes.Api;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Net;
using Xunit;
using ProjetoClientes.Domain.Dto;
using ProjetoClientes.Domain.Interfaces.Repositories;
using System.Linq;
using ProjetoClientes.Infra.Repositories;
using ProjetoClientes.Infra.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using ProjetoClientes.Domain.Helper;

namespace ProjetoClientes.Test
{
 
    public class ClienteTest 
    {

        [Fact]
        public void ValidaCPF()
        {
            //Arrange
            
            const string cpf= "00000000000";

            //Act
            bool isValid = Util.ValidaCPF(cpf);

            //Assert
            Assert.True(isValid, $"O CPF {cpf} não é valido");
        }


    }
}

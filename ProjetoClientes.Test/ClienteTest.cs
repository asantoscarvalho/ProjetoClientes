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
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;
using ProjetoClientes.Domain.Entities;
using System.Text.Json;
using System.Threading;

namespace ProjetoClientes.Test
{
 
    public class ClienteTest
    { 



        [Theory]
        [InlineData("04258358711", true)]
        [InlineData("00000000000", false)]
        [InlineData("24563481222", false)]
        [InlineData("32156456564", false)]
        public void ValidaCPF(string cpf, bool valido)
        {
            //Arrange
            
            

            //Act
            bool isValid = Util.ValidaCPF(cpf);

            //Assert
            Assert.Equal(valido,isValid);
        }


    }
}

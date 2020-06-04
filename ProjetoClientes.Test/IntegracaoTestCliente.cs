using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProjetoClientes.Api;
using ProjetoClientes.Domain.Dto;
using ProjetoClientes.Domain.Entities;
using ProjetoClientes.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
namespace ProjetoClientes.Test
{

    public class IntegracaoTestCliente : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;

        public int ClienteID { get; set; } = 1;

        public IntegracaoTestCliente(WebApplicationFactory<Startup> factory)
        {
            httpClient = factory.CreateClient();
        }


        [Fact]
        [Trait("Post", "Cliente")]
        public async Task Post()
        {
            ClienteDto _clienteDto = new ClienteDto() { 
                Nome = "João da Silva",
                DataNascimento = DateTime.Now,
                Cpf = "30805051074"
            };

            string strClienteDto = JsonConvert.SerializeObject(_clienteDto);

            HttpContent contet = new StringContent(strClienteDto, Encoding.UTF8, "application/json");
            
            var response = await httpClient.PostAsync("api/cliente/", contet);

            
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var cliente = System.Text.Json.JsonSerializer.Deserialize<Cliente>(stringResponse, new  JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            ClienteID = cliente.Id;
            Assert.Equal(_clienteDto.Nome, cliente.Nome);

        }


        
       
       
        [Fact]
        [Trait("Delete", "Cliente")]
        public async Task Delete()
        {

            var responseGet = await httpClient.GetAsync("api/cliente");


            responseGet.EnsureSuccessStatusCode();
            var stringResponse = await responseGet.Content.ReadAsStringAsync();
            var cliente = System.Text.Json.JsonSerializer.Deserialize<List<Cliente>>(stringResponse, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase });

            var Id = cliente.LastOrDefault().Id;

            var response = await httpClient.DeleteAsync($"api/cliente/{Id}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);

        }


        [Fact]
        [Trait("Cliente","Nome Invalido")]
        public async Task Nao_Cadastrar_sem_nome_preenchido()
        {
            ClienteDto _clienteDto = new ClienteDto()
            {
                Nome = "",
                DataNascimento = DateTime.Now,
                Cpf = "30805051074"
            };

            string strClienteDto = JsonConvert.SerializeObject(_clienteDto);

            HttpContent contet = new StringContent(strClienteDto, Encoding.UTF8, "application/json");
            
            var response = await httpClient.PostAsync("api/cliente/", contet);

            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);


        }

        [Fact]
        [Trait("Cliente","Data Nascimento Invalido")]
        public async Task Nao_Cadastrar_sem_Data_Nascimento_preenchido()
        {
            ClienteDto _clienteDto = new ClienteDto()
            {
                Nome = "João",
                DataNascimento = Convert.ToDateTime(null),
                Cpf = "30805051074"
            };

            string strClienteDto = JsonConvert.SerializeObject(_clienteDto);

            HttpContent contet = new StringContent(strClienteDto, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/cliente/", contet);

            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);


        }

        [Fact]
        [Trait("CPF Invalido", "Cliente")]
        public async Task Nao_Cadastrar_com_cpf_invalido()
        {
            ClienteDto _clienteDto = new ClienteDto()
            {
                Nome = "João",
                DataNascimento = DateTime.Now,
                Cpf = "213156465"
            };

            string strClienteDto = JsonConvert.SerializeObject(_clienteDto);

            HttpContent contet = new StringContent(strClienteDto, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/cliente/", contet);

            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);


        }

        [Fact]
        [Trait("GetAll","Cliente")]
        public async Task GetAll()
        {
            
            var response = await httpClient.GetAsync("api/cliente");

            
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var cliente = System.Text.Json.JsonSerializer.Deserialize<List<Cliente>>(stringResponse, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase });

            Assert.NotEmpty(cliente);

        }



    }
}

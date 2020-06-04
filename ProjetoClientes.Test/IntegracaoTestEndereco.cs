using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProjetoClientes.Api;
using ProjetoClientes.Domain.Dto;
using ProjetoClientes.Domain.Entities;
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
    
    public class IntegracaoTestEndereco : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;

        

        public IntegracaoTestEndereco(WebApplicationFactory<Startup> factory)
        {
            httpClient = factory.CreateClient();
        }


        [Fact]
        [Trait("Post", "Endereco")]
    
        public async Task Post()
        {

            var responseGet = await httpClient.GetAsync("api/cliente");


            responseGet.EnsureSuccessStatusCode();
            var clientstringResponse = await responseGet.Content.ReadAsStringAsync();
            var cliente = System.Text.Json.JsonSerializer.Deserialize<List<Cliente>>(clientstringResponse, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase });

            var ClientId = cliente.LastOrDefault().Id;

            EnderecoDto _EnderecoDto = new EnderecoDto() { 
                Logradouro = "Rua João da Silva",
                Bairro = "Bairro ",
                Cidade = "Rio de Janeiro",
                Estado = "Rio de Janeiro",
                ClienteId = ClientId
            };

            string strEnderecoDto = JsonConvert.SerializeObject(_EnderecoDto);

            HttpContent contet = new StringContent(strEnderecoDto, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/endereco/", contet);

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var Endereco = System.Text.Json.JsonSerializer.Deserialize<Endereco>(stringResponse, new  JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            
            Assert.Equal(_EnderecoDto.Logradouro, Endereco.Logradouro);

        }


        
       
        
        [Fact]
        [Trait("Delete", "Endereco")]
        public async Task Delete()
        {

            var responseGet = await httpClient.GetAsync("api/endereco");


            responseGet.EnsureSuccessStatusCode();
            var stringResponse = await responseGet.Content.ReadAsStringAsync();

            Assert.NotEmpty(stringResponse);
            var cliente = System.Text.Json.JsonSerializer.Deserialize<List<Endereco>>(stringResponse, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase });

            var Id = cliente.LastOrDefault().Id;
            var response = await httpClient.DeleteAsync($"api/endereco/{Id}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(StatusCodes.Status200OK , (int)response.StatusCode);

        }


        [Fact]
        [Trait("Logradouro Invalido", "Endereco")]
        public async Task Nao_Cadastrar_sem_logradouro()
        {
            EnderecoDto _EnderecoDto = new EnderecoDto()
            {
                Logradouro = "",
                Bairro = "Bairro ",
                Cidade = "Rio de Janeiro",
                Estado = "Rio de Janeiro",
                ClienteId = 1
            };

            string strEnderecoDto = JsonConvert.SerializeObject(_EnderecoDto);

            HttpContent contet = new StringContent(strEnderecoDto, Encoding.UTF8, "application/json");
            
            var response = await httpClient.PostAsync("api/endereco/", contet);


            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);

        }

        [Fact]
        [Trait("Bairro Invalido", "Endereco")]
        public async Task Nao_Cadastrar_sem_bairro_preenchido()
        {
            EnderecoDto _EnderecoDto = new EnderecoDto()
            {
                Logradouro = "Rua João",
                Bairro = "",
                Cidade = "Rio de Janeiro",
                Estado = "Rio de Janeiro",
                ClienteId = 1
            };

            string strEnderecoDto = JsonConvert.SerializeObject(_EnderecoDto);

            HttpContent contet = new StringContent(strEnderecoDto, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/Endereco/", contet);

            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);


        }

        [Fact]
        [Trait("Cidade Invalido", "Endereco")]
        public async Task Nao_Cadastrar_sem_Cidade()
        {
            EnderecoDto _EnderecoDto = new EnderecoDto()
            {
                Logradouro = "Rua João",
                Bairro = "Bairro",
                Cidade = "",
                Estado = "Rio de Janeiro",
                ClienteId = 1
            };

            string strEnderecoDto = JsonConvert.SerializeObject(_EnderecoDto);

            HttpContent contet = new StringContent(strEnderecoDto, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/Endereco/", contet);

            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);

        }

        [Fact]
        [Trait("Estado Invalido", "Endereco")]
        public async Task Nao_Cadastrar_sem_Estado()
        {
            EnderecoDto _EnderecoDto = new EnderecoDto()
            {
                Logradouro = "Rua João",
                Bairro = "Bairro",
                Cidade = "Rio de Janeiro",
                Estado = "",
                ClienteId = 1
            };

            string strEnderecoDto = JsonConvert.SerializeObject(_EnderecoDto);

            HttpContent contet = new StringContent(strEnderecoDto, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/Endereco/", contet);

            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);
        }

        [Fact]
        [Trait("Cliente Invalido", "Endereco")]
        public async Task Nao_Cadastrar_sem_Cliente()
        {
            EnderecoDto _EnderecoDto = new EnderecoDto()
            {
                Logradouro = "Rua João",
                Bairro = "Bairro",
                Cidade = "",
                Estado = "Rio de Janeiro",
                ClienteId = 0
            };

            string strEnderecoDto = JsonConvert.SerializeObject(_EnderecoDto);

            HttpContent contet = new StringContent(strEnderecoDto, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/Endereco/", contet);

            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);

        }


        [Fact]
        [Trait("GetAll","Endereco")]
        public async Task GetAll()
        {
            var response = await httpClient.GetAsync("api/Endereco");

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var Endereco = System.Text.Json.JsonSerializer.Deserialize<List<Endereco>>(stringResponse, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase });

            Assert.NotEmpty(Endereco);

        }



    }
}

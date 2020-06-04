using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoClientes.Domain.Dto;
using ProjetoClientes.Domain.Entities;
using ProjetoClientes.Domain.Interfaces.Repositories;

namespace ProjetoClientes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repo;

        private readonly IMapper _mapper;

        public ClienteController(IClienteRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        /// <summary>
        /// Lista todos os Clientes cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
        {
            try
            {
                var clientes = await _repo.GetAll();

                var results = _mapper.Map<ClienteDto[]>(clientes);

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna o cliente cadastrado de acordo com o codigo informado.
        /// </summary>
        /// <param name="ClienteId"></param>  
        [HttpGet("{ClienteId}")]
        public async Task<ActionResult> Get(int ClienteId)
        {
            try
            {
                var cliente = await _repo.GetById(ClienteId);

                var result = _mapper.Map<ClienteDto>(cliente);

                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Grava os dados do cliente.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post(ClienteDto model)
        {
            try
            {
                var modelo = _mapper.Map<Cliente>(model);

                _repo.Add(modelo);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/cliente/{model.Id}", _mapper.Map<ClienteDto>(modelo));
                }


            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();

        }

        /// <summary>
        /// Alterar dados do cliente.
        /// </summary>
        /// <param name="ClienteId"></param>  
        /// <param name="model"></param>  
        [HttpPut("{ClienteId}")]
        public async Task<ActionResult> Put(int ClienteId, ClienteDto model)
        {
            try
            {
                var cliente = await _repo.GetById(ClienteId);
                if (cliente == null) return NotFound();



                if (cliente.enderecos != null && cliente.enderecos.Count > 0 )
                {
                    var idEnderecos = new List<int>();

                    foreach (var item in model.enderecos)
                    {
                        idEnderecos.Add(item.Id);
                    }



                    var enderecos = cliente.enderecos.Where(end => !idEnderecos.Contains(end.Id)).ToArray();

                    if (enderecos.Length > 0)
                        _repo.DeleteRange(enderecos);


                }
                

                
                _mapper.Map(model, cliente);

                _repo.Update(cliente);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/cliente/{model.Id}", _mapper.Map<ClienteDto>(cliente));
                }


            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();

        }

        /// <summary>
        /// Excluir o cliente de acordo com o código informado.
        /// </summary>
        /// <param name="ClienteId"></param>  

        [HttpDelete("{ClienteId}")]
        public async Task<ActionResult> Delete(int ClienteId)
        {
            try
            {
                var cliente = await _repo.GetById(ClienteId);
                if (cliente == null) return NotFound();

                _repo.Delete(cliente);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }


            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();

        }


    }
}

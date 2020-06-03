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
    public class EnderecoController : ControllerBase
    {
        private IEnderecoRepository _repo;

        private readonly IMapper _mapper;

        public EnderecoController(IEnderecoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todos os endereços cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoDto>>> Get()
        {
            try
            {
                var enderecos = await _repo.GetAll();

                var results = _mapper.Map<EnderecoDto[]>(enderecos);

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna o endereço cadastrado de acordo com o codigo informado.
        /// </summary>
        /// <param name="EnderecoId"></param>  
        [HttpGet("{EnderecoId}")]
        public async Task<ActionResult> Get(int EnderecoId)
        {
            try
            {
                var endereco = await _repo.GetById(EnderecoId);

                var result = _mapper.Map<EnderecoDto>(endereco);

                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Grava os dados do endereço.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post(EnderecoDto model)
        {
            try
            {
                var modelo = _mapper.Map<Endereco>(model);

                _repo.Add(modelo);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/endereco/{model.Id}", _mapper.Map<EnderecoDto>(modelo));
                }


            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();

        }

        /// <summary>
        /// Alterar dados do Endereço.
        /// </summary>
        /// <param name="EnderecoId"></param>  
        /// <param name="model"></param>  

        [HttpPut("{EnderecoId}")]
        public async Task<ActionResult> Put(int EnderecoId, EnderecoDto model)
        {
            try
            {
                var endereco = await _repo.GetById(EnderecoId);
                if (endereco == null) return NotFound();
                
                _mapper.Map(model, endereco);

                _repo.Update(endereco);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/endereco/{model.Id}", _mapper.Map<EnderecoDto>(endereco));
                }


            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();

        }
        /// <summary>
        /// Excluir o endereço do código informado.
        /// </summary>
        /// <param name="EnderecoId"></param>  
        [HttpDelete("{EnderecoId}")]
        public async Task<ActionResult> Delete(int EnderecoId)
        {
            try
            {
                var endereco = await _repo.GetById(EnderecoId);
                if (endereco == null) return NotFound();

                _repo.Delete(endereco);

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

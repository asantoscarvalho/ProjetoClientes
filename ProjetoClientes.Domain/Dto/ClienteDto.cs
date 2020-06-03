using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjetoClientes.Domain.CustomValidations;

namespace ProjetoClientes.Domain.Dto
{
    public class ClienteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength:30, ErrorMessage = "O campo {0} é obrigatório e deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [CpfValidationAttribute(ErrorMessage="O numero do cpf é invalido")]
        public string Cpf { get; set; }

        [Required(ErrorMessage="O campo Data de Nascimento é obrigatório")]
        [DataValidationAttribute(ErrorMessage = "Data Invalida ou não informada")]
        public DateTime DataNascimento { get; set; }

        private int _idade = 0;

        public int Idade
        {
            get
            {
                if (this.DataNascimento != null)
                {
                    _idade =(DateTime.Now.DayOfYear > this.DataNascimento.DayOfYear) ?  DateTime.Now.Year - this.DataNascimento.Year : (DateTime.Now.Year - this.DataNascimento.Year)-1;

                }
                return _idade;
            }
            set
            {
                _idade = value;
            }
        }
        public List<EnderecoDto> enderecos {get; set;}
        
    }
}
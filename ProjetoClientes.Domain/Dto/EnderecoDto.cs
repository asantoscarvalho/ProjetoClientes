using System.ComponentModel.DataAnnotations;

namespace ProjetoClientes.Domain.Dto
{
    public class EnderecoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength:50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength:40, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        public string Bairro { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength:40, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(maximumLength:40, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O campo Cliente é obrigatório.")]
        public int ClienteId { get; set; }
        public ClienteDto Cliente {get; set;}
        
    }
}
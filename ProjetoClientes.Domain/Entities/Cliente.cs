using System;
using System.Collections.Generic;

namespace ProjetoClientes.Domain.Entities
{
    public class Cliente: EntityBase
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }

        public List<Endereco> enderecos {get; set;}
        
    }
}
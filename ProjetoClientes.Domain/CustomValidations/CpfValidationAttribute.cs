using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjetoClientes.Domain.Helper;

namespace ProjetoClientes.Domain.CustomValidations
{
    public class CpfValidationAttribute : ValidationAttribute
    {

        public CpfValidationAttribute() { }
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            return false;

            bool valido = Util.ValidaCPF(value.ToString());
            return valido;
        }

    }

}
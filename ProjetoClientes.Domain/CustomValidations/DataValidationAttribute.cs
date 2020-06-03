using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjetoClientes.Domain.Helper;


namespace ProjetoClientes.Domain.CustomValidations
{
    public class DataValidationAttribute : ValidationAttribute
    {
        public DataValidationAttribute() { }
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;

            bool valido = Util.ValidaData((DateTime)value);
            return valido;
        }
    }
}

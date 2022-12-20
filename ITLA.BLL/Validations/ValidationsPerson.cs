using ITLA.BLL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITLA.BLL.Validations
{
    public static class ValidationsPerson
    {
        public static ServiceResult IsValidPerson(PersonDto person)
        {
            ServiceResult result = new ServiceResult();

            if (string.IsNullOrEmpty(person.FirstName))
            {
                result.Success = false;
                result.Message = "El nombre del estudiante es requerido.";
                return result;
            }

            if (string.IsNullOrEmpty(person.LastName))
            {
                result.Success = false;
                result.Message = "El apellido del estudiante es requerido.";
                return result;
            }

            if (person.FirstName.Length > 50)
            {
                result.Success = false;
                result.Message = "La longitud del nombre es inválida.";
                return result;
            }

            if (person.LastName.Length > 50)
            {
                result.Success = false;
                result.Message = "La longitud del apellido es inválida.";
                return result;
            }

            //if (string.IsNullOrEmpty(person.Email))
            //{
            //    result.Success = false;
            //    result.Message = "El correo electronico es requerido";
            //    return result;
            //}

            return result;
        }
    }
}

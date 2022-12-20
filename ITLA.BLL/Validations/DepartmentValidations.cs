using System;
using System.Collections.Generic;
using System.Text;
using ITLA.BLL.Core;
using ITLA.DAL.Entities;
using ITLA.DAL.Interfaces;

namespace ITLA.BLL.Validations
{
    public class DepartmentValidations
    {
        public static ServiceResult IsValidDepartment(DtoDepartmentBase department,IDepartmentRepository departmentRepository)
        {
            DAL.Entities.Department departmentToEvaluate = departmentRepository.GetEntity(department.Id);

            ServiceResult result = new ServiceResult();
            if (string.IsNullOrEmpty(department.Name))
            {
                result.Success = false;
                result.Message = "El nombre de la carrera es necesario.";
                return result;
            }
            if(department.Name.Length > 30)
            {
                result.Success=false;
                result.Message = "El nombre de la carrera es muy largo";
                return result;
            }
            if (departmentRepository.Exists(st => st.Name == department.Name && st.DepartmentID == department.Id))
            {
                result.Success = false;
                result.Message = "La carrera ya existe, no se puede agregar nuevamente";
                return result;
            }


            return result;
        }
    }
}

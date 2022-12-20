using System;
using System.Collections.Generic;
using System.Text;
using ITLA.BLL.Core;
using ITLA.DAL.Interfaces;

namespace ITLA.BLL.Validations
{
    public class CourseValidations
    {
        public static ServiceResult IsValidCourse(DtoCourseBase course, ICourseRepository courseRepository)
        {
            DAL.Entities.Course courseToEvaluate = courseRepository.GetEntity(course.DepartmentId);

            ServiceResult result = new ServiceResult();
            if (string.IsNullOrEmpty(course.Title))
            {
                result.Success = false;
                result.Message = "El nombre de la asignatura es requerido.";
                return result;
            }

            if (course.Credits < 0)
            {
                result.Success = false;
                result.Message = "La cantidad de creditos de una asignatura no puede ser menor que 0";
                return result;
            }

            if(course.Credits > 5)
            {
                result.Success = false;
                result.Message = "La cantidad de creditos no puede ser mayor a 5";
                return result;
            }

            if (courseRepository.Exists(st => st.Title == course.Title && st.DepartmentID == course.DepartmentId))
            {
                result.Success = false;
                result.Message = "La asignatura ya existe, no se puede agregar nuevamente";
                return result;
            }
            return result;
        }
    }
}

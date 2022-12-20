using ITLA.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using ITLA.DAL.Interfaces;
using ITLA.BLL.Core;
using ITLA.BLL.Dtos;
using ITLA.BLL.Responses;
using System.Linq;
using ITLA.DAL.Entities;
using ITLA.BLL.Validations;
using ITLA.BLL.Models;

namespace ITLA.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly ILoggerService<CourseService> loggerService;

        public CourseService(ICourseRepository courseRespository,
                              IDepartmentRepository departmentRepository,
                              ILoggerService<CourseService> loggerService
                              )
        {
            this.courseRepository = courseRespository;
            this.departmentRepository = departmentRepository;
            this.loggerService = loggerService;
        }
        public ServiceResult GetAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var query = (from course in this.courseRepository.GetEntities()
                             select new Models.CourseModel()
                             {
                                 Id = course.CourseID,
                                 Title = course.Title,
                                 Credits = course.Credits,
                                 DepartmentId = course.DepartmentID,
                                 Deleted = course.Deleted,
                                 UserDeleted = course.UserDeleted,
                                 DeletedDate = course.DeletedDate

                             }).ToList();

                result.Data = query;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error obteniendo las asignatura";
                this.loggerService.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public ServiceResult GetById(int Id)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                DAL.Entities.Course course = courseRepository.GetEntity(Id);

                CourseModel model = new CourseModel()
                {
                    Id = course.CourseID,
                    Credits = course.Credits,
                    Title = course.Title,
                    DepartmentId= course.DepartmentID
                    
                };

                result.Data = model;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = "Ocurrio un error obteniendo la asignatura";
                this.loggerService.LogError(result.Message, ex.ToString());
            }

            return result;
        }
        public ServiceResult RemoveCourse(CourseRemoveDto courseRemovedto)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                DAL.Entities.Course courseToRemove = courseRepository.GetEntity(courseRemovedto.Id); // Se busca el estudiante a eliminar //

                courseToRemove.CourseID = courseRemovedto.Id;
                courseToRemove.UserDeleted = courseRemovedto.UserDeleted;
                courseToRemove.Deleted = true;
                courseToRemove.DeletedDate = DateTime.Now;
                courseRepository.Remove(courseToRemove);

                result.Message = "Asignatura eliminada correctamente";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error eliminando la asignatura";
                this.loggerService.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }

            return result;
        }


        public CourseResponse GetCourses()
        {
            throw new NotImplementedException();
        }

        public CourseSaveResponse SaveCourse(CourseSaveDto courseSaveDto)
        {
            CourseSaveResponse result = new CourseSaveResponse();

            try
            {
                var resutlIsValid = CourseValidations.IsValidCourse(courseSaveDto, courseRepository);

                if (resutlIsValid.Success)
                {

                    DAL.Entities.Course courseToAdd = new DAL.Entities.Course()
                    {
                        CourseID = courseSaveDto.Id,
                        Title = courseSaveDto.Title,
                        Credits = courseSaveDto.Credits,
                        DepartmentID = courseSaveDto.DepartmentId,
                        CreationDate = DateTime.Now,
                        CreationUser = courseSaveDto.UserId
                    };

                    courseRepository.Save(courseToAdd);


                    result.Message = "Asignatura agregada correctamente";

                }
                else
                {
                    result.Success = resutlIsValid.Success;
                    result.Message = resutlIsValid.Message;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error eliminando la asignatura";
                this.loggerService.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }
            return result;
        }

        public CourseUpdateResponse UpdateCourse(CourseUpdateDto courseUpdateDto)
        {
            CourseUpdateResponse result = new CourseUpdateResponse();

            try
            {

                var resultIsValid = CourseValidations.IsValidCourse(courseUpdateDto, courseRepository);

                if (resultIsValid.Success)
                {
                        DAL.Entities.Course courseToUpdate = courseRepository.GetEntity(courseUpdateDto.Id); // Se busca el estudiante a actualizar //

                        courseToUpdate.Title = courseUpdateDto.Title;
                        courseToUpdate.Credits = courseUpdateDto.Credits;
                        courseToUpdate.CourseID = courseUpdateDto.Id;
                        courseToUpdate.ModifyDate = DateTime.Now;
                        courseToUpdate.UserMod = courseUpdateDto.UserId;
                        courseToUpdate.DepartmentID = courseUpdateDto.DepartmentId;

                        courseRepository.Update(courseToUpdate);

                        result.Message = "asignatura actualizada correctamente";

                }
                else
                {
                    result.Success = false;
                    result.Message = resultIsValid.Message;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error actualizando la asigntura";
                this.loggerService.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }

            return result;
        }
    }
}

using ITLA.BLL.Contracts;
using System;
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
    public class DepartmentService : IDepartmentService
    {
        private readonly ICourseRepository courseRespository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly ILoggerService<DepartmentService> loggerService;

        public DepartmentService(ICourseRepository courseRespository,
                              IDepartmentRepository departmentRepository,
                              ILoggerService<DepartmentService> loggerService
                              )
        {
            this.courseRespository = courseRespository;
            this.departmentRepository = departmentRepository;
            this.loggerService = loggerService;
        }
        public ServiceResult GetAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var query = (from department in this.departmentRepository.GetEntities()
                             select new Models.DepartmentModel()
                             {
                                 Name = department.Name,
                                 DepartmentID = department.DepartmentID,
                                 Budget = department.Budget,
                                 StartDate = department.StartDate,
                                 Administrator = department.Administrator,
                                 Deleted = department.Deleted,
                                 UserDeleted = department.UserDeleted,
                                 DeletedDate = department.DeletedDate
                             }).ToList();

                result.Data = query;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error obteniendo las carreras";
                this.loggerService.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public ServiceResult GetById(int Id)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                Department department = departmentRepository.GetEntity(Id);

                DepartmentModel model = new DepartmentModel()
                {
                    DepartmentID = department.DepartmentID,
                    Administrator = department.Administrator,
                    Name = department.Name,
                    Budget = department.Budget,
                    StartDate = department.StartDate.Value,
                    StartDateDisplay = department.StartDate.Value.ToString("dd/mm/yyyy")
                };

                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obteniendo el Id de la asignatura.";
                this.loggerService.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public ServiceResult GetDepartmentById()
        {
            throw new NotImplementedException();
        }

        public ServiceResult RemoveDepartment(DepartmentRemoveDto removeDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                DAL.Entities.Department departmentToRemove = departmentRepository.GetEntity(removeDto.Id); // Se busca el estudiante a eliminar //

                departmentToRemove.DepartmentID = removeDto.Id;
                departmentToRemove.UserDeleted = removeDto.UserDeleted;
                departmentToRemove.Deleted = true;
                departmentToRemove.DeletedDate = DateTime.Now;
                departmentRepository.Remove(departmentToRemove);

                result.Message = "Carrera eliminada correctamente";
                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error eliminando la carrera";
                this.loggerService.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }

            return result;
        }

        public DepartmentSaveResponse SaveDepartment(DepartmentSaveDto departmentSaveDto)
        {
            DepartmentSaveResponse result = new DepartmentSaveResponse();

            try
            {
                var resutlIsValid = DepartmentValidations.IsValidDepartment(departmentSaveDto, departmentRepository);

                if (resutlIsValid.Success)
                {

                    DAL.Entities.Department departmentToAdd = new DAL.Entities.Department()
                    {
                        DepartmentID = departmentSaveDto.Id,
                        Name = departmentSaveDto.Name,
                        Budget = departmentSaveDto.Budget,
                        StartDate = DateTime.Now,
                        Administrator = departmentSaveDto.Administrator,
                        CreationUser = departmentSaveDto.UserId
                    };

                    departmentRepository.Save(departmentToAdd);

                    //result.Matricula = studentToAdd.Id.ToString();

                    result.Message = "Carrera agregada correctamente";

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
                result.Message = "Error eliminando la carrera";
                this.loggerService.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }
            return result;
        }

        public DepartmentUpdateResponse UpdateDepartment(DepartmentUpdateDto departmentUpdateDto)
        {
            DepartmentUpdateResponse result = new DepartmentUpdateResponse();

            try
            {
                var resultIsValid = DepartmentValidations.IsValidDepartment(departmentUpdateDto, departmentRepository);

                if (resultIsValid.Success)
                {
                        DAL.Entities.Department departmentToUpdate = departmentRepository.GetEntity(departmentUpdateDto.Id); // Se busca el estudiante a actualizar //

                        departmentToUpdate.Name = departmentUpdateDto.Name;
                        departmentToUpdate.Budget = departmentUpdateDto.Budget;
                        departmentToUpdate.Administrator = departmentUpdateDto.Administrator;
                        departmentToUpdate.ModifyDate = DateTime.Now;
                        departmentToUpdate.UserMod = departmentUpdateDto.UserId;
                        departmentToUpdate.StartDate = departmentUpdateDto.StartDate;
                        departmentToUpdate.DepartmentID = departmentUpdateDto.Id;

                        departmentRepository.Update(departmentToUpdate);

                        result.Message = "Carrera actualizada correctamente";

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
                result.Message = "Error actualizando la carrera";
                this.loggerService.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }
            return result;
        }
    }
}



using Microsoft.Extensions.Logging;
using ITLA.DAL.Interfaces;
using ITLA.BLL.Contracts;
using ITLA.BLL.Core;
using ITLA.BLL.Dtos;
using ITLA.BLL.Models;
using ITLA.BLL.Responses;
using ITLA.BLL.Validations;
using System;
using System.Linq;

namespace ITLA.BLL.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorsRepository instructorRepository;
        private readonly ILogger<InstructorService> logger;
        public InstructorService(IInstructorsRepository instructorRepository,
                              ILogger<InstructorService> logger)
        {
            this.instructorRepository = instructorRepository;
            this.logger = logger;
        }
        public ServiceResult GetAll()
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var instructors = instructorRepository.GetEntities();

                result.Data = instructors.Select(st => new Models.InstructorModel()
                {
                    Id = st.Id,
                    FirstName = st.FirstName,
                    LastName = st.LastName,
                    HireDate = st.HireDate,
                    Deleted = st.Deleted,
                    UserDeleted = st.UserDeleted,
                    DeletedDate = st.DeletedDate
                }).ToList();

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obtiendo los profesores";
                this.logger.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }

            return result;
        }
        public ServiceResult GetById(int Id)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                DAL.Entities.Instructors instructor = instructorRepository.GetEntity(Id);

                InstructorModel model = new InstructorModel()
                {
                    Id = instructor.Id,
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
                    HireDate= instructor.HireDate.Value,
                    HireDateDisplay = instructor.HireDate.Value.ToString("dd/mm/yyyy"),
                };

                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obteniendo el profesor del Id.";
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public ServiceResult DeleteInstructor(InstructorRemoveDto instructorRemoveDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                DAL.Entities.Instructors instructorToRemove = instructorRepository.GetEntity(instructorRemoveDto.Id); // Se busca el estudiante a eliminar //

                instructorToRemove.Id = instructorRemoveDto.Id;
                instructorToRemove.UserDeleted = instructorRemoveDto.UserDeleted;
                instructorToRemove.Deleted = true;
                instructorToRemove.DeletedDate = DateTime.Now;

                instructorRepository.Remove(instructorToRemove);

                result.Message = "Profesor eliminado correctamente";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error eliminando el profesor";
                this.logger.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }

            return result;
        }



        public InstructorResponse GetInstructors()
        {
            throw new NotImplementedException();
        }

        public InstructorSaveResponse SaveInstructor(InstructorSaveDto instructorSaveDto)
        {
            InstructorSaveResponse result = new InstructorSaveResponse();

            try
            {

                var resutlIsValid = ValidationsPerson.IsValidPerson(instructorSaveDto);

                if (resutlIsValid.Success)
                {
                    if (instructorSaveDto.HireDate.HasValue)
                    {
                        if (instructorRepository.Exists(st => st.FirstName == instructorSaveDto.FirstName
                                                        && st.LastName == instructorSaveDto.LastName
                                                        && st.HireDate == instructorSaveDto.HireDate
                                                        ))
                        {
                            result.Success = false;
                            result.Message = "Este profesor ya se encuentra registrado.";
                            return result;
                        }

                        DAL.Entities.Instructors instructorToAdd = new DAL.Entities.Instructors()
                        {
                            Id = instructorSaveDto.Id,
                            LastName = instructorSaveDto.LastName,
                            HireDate = (DateTime)instructorSaveDto.HireDate,
                            FirstName = instructorSaveDto.FirstName,
                            CreationDate = DateTime.Now,
                            CreationUser = instructorSaveDto.UserId
                        };

                        instructorRepository.Save(instructorToAdd);

                        result.Id = instructorToAdd.Id;

                        result.Message = "Profesor agregado correctamente";

                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "La fecha de contratacion es requerida.";
                        return result;
                    }
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
                result.Message = "Error agregando el profesor";
                this.logger.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }
            return result;
        }

        public InstructorUpdateResponse UpdateInstructor(InstructorUpdateDto instructorUpdateDto)
        {
            InstructorUpdateResponse result = new InstructorUpdateResponse();

            try
            {

                var resultIsValid = ValidationsPerson.IsValidPerson(instructorUpdateDto);

                if (resultIsValid.Success)
                {

                    if (instructorUpdateDto.HireDate.HasValue)
                    {
                        DAL.Entities.Instructors instructorToUpdate = instructorRepository.GetEntity(instructorUpdateDto.Id); // Se busca el estudiante a actualizar //

                        instructorToUpdate.FirstName = instructorUpdateDto.FirstName;
                        instructorToUpdate.LastName = instructorUpdateDto.LastName;
                        instructorToUpdate.HireDate = instructorUpdateDto.HireDate;
                        instructorToUpdate.ModifyDate = DateTime.Now;
                        instructorToUpdate.UserMod = instructorUpdateDto.UserId;
                        instructorToUpdate.Id = instructorUpdateDto.Id;

                        instructorRepository.Update(instructorToUpdate);

                        result.Message = "Profesor actualizado correctamente";
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "La fecha de contratación es requerida.";
                        return result;
                    }

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
                result.Message = "Error actualizando el profesor";
                this.logger.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }

            return result;
        }
    }
}

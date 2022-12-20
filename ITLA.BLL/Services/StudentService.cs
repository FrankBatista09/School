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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;
        private readonly ILogger<StudentService> logger;
        public StudentService(IStudentRepository studentRepository,
                              ILogger<StudentService> logger)
        {
            this.studentRepository = studentRepository;
            this.logger = logger;
        }
        public ServiceResult GetAll()
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var students = studentRepository.GetEntities();

                result.Data = students.Select(st => new Models.StudentModel()
                {
                    Id = st.Id,
                    EnrollmentDate = st.EnrollmentDate,
                    EnrollmentDateDisplay = st.EnrollmentDate.Value.ToString("dd/mm/yyyy"),
                    FirstName = st.FirstName,
                    LastName = st.LastName,
                    Deleted = st.Deleted,
                    UserDeleted = st.UserDeleted,
                    DeletedDate = st.DeletedDate
                }).ToList();

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obtiendo los estudiantes";
                this.logger.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }

            return result;
        }

        public ServiceResult GetById(int Id)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                DAL.Entities.Students student = studentRepository.GetEntity(Id);

                StudentModel model = new StudentModel()
                {
                    EnrollmentDate = student.EnrollmentDate.Value,
                    EnrollmentDateDisplay = student.EnrollmentDate.Value.ToString("dd/mm/yyyy"),
                    FirstName = student.FirstName,
                    Id = student.Id,
                    LastName = student.LastName
                };

                result.Data = model;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error obteniendo el id del estudiante.";
                this.logger.LogError(result.Message, ex.ToString());
            }
            return result;
        }

        public ServiceResult GetStudentsGrades()
        {
            throw new NotImplementedException();
        }

        public ServiceResult RemoveStudent(StudentRemoveDto removeDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                DAL.Entities.Students studentToRemove = studentRepository.GetEntity(removeDto.Id); // Se busca el estudiante a eliminar //

                studentToRemove.Id = removeDto.Id;
                studentToRemove.UserDeleted = removeDto.UserDeleted;
                studentToRemove.Deleted = true;
                studentToRemove.DeletedDate = DateTime.Now;
                studentRepository.Remove(studentToRemove);

                result.Message = "Estudiante eliminado correctamente";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error eliminando el estudiante";
                this.logger.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }

            return result;
        }

        public StudentSaveResponse SaveStudent(StudentSaveDto studentSaveDto)
        {
            StudentSaveResponse result = new StudentSaveResponse();

            try
            { 

                var resutlIsValid = ValidationsPerson.IsValidPerson(studentSaveDto);

                if (resutlIsValid.Success)
                {
                    if (studentSaveDto.EnrollmentDate.HasValue)
                    {
                        if (studentRepository.Exists(st => st.FirstName == studentSaveDto.FirstName
                                                        && st.LastName == studentSaveDto.LastName
                                                        && st.EnrollmentDate.Value.Date == studentSaveDto.EnrollmentDate.Value.Date
                                                        ))
                        {
                            result.Success = false;
                            result.Message = "Este estudiante ya se encuentra registrado.";
                            return result;
                        }

                        DAL.Entities.Students studentToAdd = new DAL.Entities.Students()
                        {
                            LastName = studentSaveDto.LastName,
                            EnrollmentDate = studentSaveDto.EnrollmentDate,
                            FirstName = studentSaveDto.FirstName,
                            CreationDate = DateTime.Now,
                            CreationUser = studentSaveDto.UserId

                        };

                        studentRepository.Save(studentToAdd);

                        result.Matricula = studentToAdd.Id.ToString();

                        result.Message = "Estudiante agregado correctamente";

                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "La fecha de inscripción es requerida.";
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
                result.Message = "Error eliminando el estudiante";
                this.logger.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }
            return result;
        }

        public StudentUpdateResponse UpdateStudent(StudentUpdateDto studentUpdateDto)
        {
            StudentUpdateResponse result = new StudentUpdateResponse();

            try
            {

                var resultIsValid = ValidationsPerson.IsValidPerson(studentUpdateDto);

                if (resultIsValid.Success)
                {

                    if (studentUpdateDto.EnrollmentDate.HasValue)
                    {
                        DAL.Entities.Students studentToUpdate = studentRepository.GetEntity(studentUpdateDto.Id); // Se busca el estudiante a actualizar //

                        studentToUpdate.FirstName = studentUpdateDto.FirstName;
                        studentToUpdate.LastName = studentUpdateDto.LastName;
                        studentToUpdate.EnrollmentDate = studentUpdateDto.EnrollmentDate;
                        studentToUpdate.ModifyDate = DateTime.Now;
                        studentToUpdate.UserMod = studentUpdateDto.UserId;
                        studentToUpdate.Id = studentUpdateDto.Id;

                        studentRepository.Update(studentToUpdate);

                        result.Message = "Estudiante actualizado correctamente";
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "La fecha de inscripción es requerida.";
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
                result.Message = "Error actualizando el estudiante";
                this.logger.LogError($" { result.Message } {ex.Message}", ex.ToString());
            }

            return result;
        }
    }
}

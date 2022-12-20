using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ITLA.DAL.Context;
using ITLA.DAL.Entities;
using ITLA.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace ITLA.DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ITLAContext context;
        private readonly ILogger<StudentRepository> logger;

        public StudentRepository(ITLAContext context,
                                ILogger<StudentRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public bool Exists(Expression<Func<Students, bool>> filter)
        {
            return context.Students.Any(filter);
        }

        public Students GetEntity(int studentId)
        {
            try
            {
                return context.Students.Find(studentId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public IEnumerable<Students> GetEntities()
        {
            try
            {
                var datos = context.Students.OrderByDescending(st => st.CreationDate).ToList();
            }
            catch (Exception ex)
            {

                throw ex ;
            }
            return context.Students.OrderByDescending(st => st.CreationDate);
        }

        public void Remove(Students student)
        {
            try
            {
                Students studentToRemove = GetEntity(student.Id);


                studentToRemove.Id = student.Id;
                studentToRemove.UserDeleted = student.UserDeleted;
                studentToRemove.Deleted = student.Deleted;
                studentToRemove.DeletedDate = DateTime.Now;

                context.Students.Update(studentToRemove);

                context.SaveChanges();

            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error removiendo el estudiante {ex.Message}", ex.ToString());
            }
        }

        public void Save(Students student)
        {
            context.Students.Add(student);
            context.SaveChanges();
        }

        public void Update(Students student)
        {
            try
            {
                Students studentToModify = GetEntity(student.Id);

                studentToModify.FirstName = student.FirstName;
                studentToModify.LastName = student.LastName;
                studentToModify.ModifyDate = student.ModifyDate;
                studentToModify.UserMod = student.UserMod;
                studentToModify.EnrollmentDate = student.EnrollmentDate;
                studentToModify.Id = student.Id;

                context.Students.Update(studentToModify);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error: { ex.Message}", ex.ToString());
            }
        }
    }
}

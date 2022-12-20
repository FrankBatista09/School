using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ITLA.DAL.Context;
using ITLA.DAL.Entities;
using ITLA.DAL.Interfaces;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ITLA.DAL.Repositories
{
    public class InstructorsRepository : IInstructorsRepository
    {
        private readonly ITLAContext context;
        private readonly ILogger<InstructorsRepository> logger;

        public InstructorsRepository(ITLAContext context, 
                                    ILogger<InstructorsRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public bool Exists(Expression<Func<Instructors, bool>> filter)
        {
            return context.Instructors.Any(filter);
        }

        public IEnumerable<Instructors> GetEntities()
        {
            return context.Instructors.OrderByDescending(st => st.CreationDate);
        }

        public Instructors GetEntity(int instructorsid)
        {
            return context.Instructors.Find(instructorsid);
        }

        public void Remove(Instructors instructors)
        {
            try
            {
                Instructors instructorToRemove = GetEntity(instructors.Id);


                instructorToRemove.Id = instructors.Id;
                instructorToRemove.UserDeleted = instructors.UserDeleted;
                instructorToRemove.Deleted = instructors.Deleted;
                instructorToRemove.DeletedDate = DateTime.Now;

                context.Instructors.Update(instructorToRemove);

                context.SaveChanges();

            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error removiendo el Instructor {ex.Message}", ex.ToString());
            }
        }

        public void Save(Instructors instructors)
        {
            context.Instructors.Add(instructors);
            context.SaveChanges();
        }

        public void Update(Instructors instructors)
        {
            try
            {
                Instructors instructorsToModify = GetEntity(instructors.Id);

                instructorsToModify.FirstName = instructors.FirstName;
                instructorsToModify.LastName = instructors.LastName;
                instructorsToModify.ModifyDate = instructors.ModifyDate;
                instructorsToModify.UserMod = instructors.UserMod;
                instructorsToModify.HireDate = instructors.HireDate;
                instructorsToModify.Id = instructors.Id;

                context.Instructors.Update(instructorsToModify);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error: { ex.Message}", ex.ToString());
            }
        }
    }
}

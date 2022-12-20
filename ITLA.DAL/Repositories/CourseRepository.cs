using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ITLA.DAL.Context;
using ITLA.DAL.Entities;
using ITLA.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ITLA.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ITLAContext context;
        private readonly ILogger<CourseRepository> logger;

        public CourseRepository(ITLAContext context,
                                 ILogger<CourseRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public bool Exists(Expression<Func<Course, bool>> filter)
        {
            return context.Courses.Any(filter);
        }

        public IEnumerable<Course> GetEntities()
        {
            return context.Courses.OrderByDescending(st => st.CreationDate);
        }

        public Course GetEntity(int courseid)
        {
            return context.Courses.Find(courseid);
        }

        public void Remove(Course course)
        {
            try
            {
                Course courseToRemove = GetEntity(course.CourseID);


                courseToRemove.CourseID = course.CourseID;
                courseToRemove.UserDeleted = course.UserDeleted;
                courseToRemove.Deleted = course.Deleted;
                courseToRemove.DeletedDate = DateTime.Now;

                context.Courses.Update(courseToRemove);

                context.SaveChanges();

            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error removiendo la asignatura {ex.Message}", ex.ToString());
            }
        }

        public void Save(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public void Update(Course course)
        {
            try
            {
                Course courseToModify = GetEntity(course.CourseID);

                courseToModify.Title = course.Title;
                courseToModify.Credits = course.Credits;
                courseToModify.DepartmentID = course.DepartmentID;
                courseToModify.CourseID = course.CourseID;
                courseToModify.UserMod = course.UserMod;
                courseToModify.ModifyDate = course.ModifyDate;


                context.Courses.Update(courseToModify);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error: { ex.Message}", ex.ToString());
            }
        }
    }
}

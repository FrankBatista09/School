using System;
using System.Collections.Generic;
using ITLA.DAL.Entities;
using ITLA.DAL.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using ITLA.DAL.Context;
using Microsoft.Extensions.Logging;

namespace ITLA.DAL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ITLAContext context;
        private readonly ILogger<DepartmentRepository> logger;

        public DepartmentRepository(ITLAContext context,
                                 ILogger<DepartmentRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public bool Exists(Expression<Func<Department, bool>> filter)
        {
            return context.Departments.Any(filter);
        }

        public IEnumerable<Department> GetEntities()
        {
            return context.Departments.OrderByDescending(st => st.CreationDate);
        }

        public Department GetEntity(int departmentid)
        {
            try
            {
                return context.Departments.Find(departmentid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void Remove(Department department)
        {
            try
            {
                Department departmentToRemove = GetEntity(department.DepartmentID);


                departmentToRemove.DepartmentID = department.DepartmentID;
                departmentToRemove.UserDeleted = department.UserDeleted;
                departmentToRemove.Deleted = department.Deleted;
                departmentToRemove.DeletedDate = DateTime.Now;

                context.Departments.Update(departmentToRemove);

                context.SaveChanges();

            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error removiendo la carrera {ex.Message}", ex.ToString());
            }
        }

        public void Save(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
        }

        public void Update(Department department)
        {
            context.Departments.Update(department);
            try
            {
                Department departmentToModify = GetEntity(department.DepartmentID);

                departmentToModify.Name = department.Name;
                departmentToModify.Budget = department.Budget;
                departmentToModify.ModifyDate = department.ModifyDate;
                departmentToModify.UserMod = department.UserMod;
                departmentToModify.StartDate = department.StartDate;
                departmentToModify.Administrator = department.Administrator;
                departmentToModify.DepartmentID = department.DepartmentID;


                context.Departments.Update(departmentToModify);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error: { ex.Message}", ex.ToString());
            }
        }
    }
}

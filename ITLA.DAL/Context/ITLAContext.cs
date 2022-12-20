using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ITLA.DAL.Entities;

namespace ITLA.DAL.Context
{
    public partial class ITLAContext: DbContext
    {
        public ITLAContext()
        {
        }
        
        public ITLAContext(DbContextOptions<ITLAContext> options) : base(options)
        {

        }
        public DbSet<Students> Students { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITLA.DAL.Entities
{
    [Table("Course", Schema = "dbo")]
    public class Course : Core.BaseEntity
    {
        public int CourseID { get; set; }
        public string? Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentID { get; set; }
    }
}

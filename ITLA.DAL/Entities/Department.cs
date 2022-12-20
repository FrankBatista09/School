using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITLA.DAL.Entities
{
    [Table("Department", Schema = "dbo")]
    public class Department : Core.BaseEntity
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime? StartDate { get; set; }
        public int Administrator { get; set; }
    }
}

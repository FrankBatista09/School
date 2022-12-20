using System;
using System.Collections.Generic;
using System.Text;

namespace ITLA.BLL.Models
{
    public class CourseModel : Core.BaseModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
      
    }
}

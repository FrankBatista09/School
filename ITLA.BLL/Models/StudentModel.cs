using System;
using System.Collections.Generic;
using System.Text;

namespace ITLA.BLL.Models
{
    public class StudentModel :Core.BaseModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string? EnrollmentDateDisplay { get; set; }
    }
}

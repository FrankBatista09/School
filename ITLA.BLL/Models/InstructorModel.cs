using System;
using System.Collections.Generic;
using System.Text;

namespace ITLA.BLL.Models
{
    public class InstructorModel : Core.BaseModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? HireDate { get; set; }
        public string? HireDateDisplay { get; set; }
    }
}

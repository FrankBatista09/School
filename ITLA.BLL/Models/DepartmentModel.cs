using System;
using System.Collections.Generic;
using System.Text;

namespace ITLA.BLL.Models
{
    public class DepartmentModel : Core.BaseModel
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime? StartDate { get; set; }
        public int Administrator { get; set; }
        public string? StartDateDisplay { get; set; }
    }
}

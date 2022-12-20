using System;

namespace ITLA.Web.Models
{
    public class Department : Base.ModelBase
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime? StartDate { get; set; }
        public int Administrator { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ITLA.BLL.Core
{
    public class DtoDepartmentBase : DtoAudit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime? StartDate { get; set; }
        public int Administrator { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ITLA.BLL.Core
{
    public class DtoAudit
    {
        public int UserId { get; set; }
        public DateTime? AuditDate { get; set; }
    }
}

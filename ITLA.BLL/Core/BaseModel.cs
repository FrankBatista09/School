using System;
using System.Collections.Generic;
using System.Text;

namespace ITLA.BLL.Core
{
    public class BaseModel
    {
        public int? UserDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool Deleted { get; set; }
    }
}

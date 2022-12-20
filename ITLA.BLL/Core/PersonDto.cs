using System;
using System.Collections.Generic;
using System.Text;

namespace ITLA.BLL.Core
{
    public class PersonDto: DtoAudit
    {
        public string? FirstName { get; set; }   
        public string? LastName { get; set; }
    }
}

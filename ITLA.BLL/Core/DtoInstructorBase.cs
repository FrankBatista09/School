using System;
using System.Collections.Generic;
using System.Text;


namespace ITLA.BLL.Core
{
    public class DtoInstructorBase: PersonDto
    {
        public DateTime? HireDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
 
namespace ITLA.BLL.Core
{
    public class DtoStudentsBase: PersonDto
    {
        public DateTime? EnrollmentDate { get; set; }
    }
}

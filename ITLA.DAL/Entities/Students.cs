using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITLA.DAL.Entities
{
    [Table("Students",Schema = "dbo")]
    public class Students: Core.Person
    {
        public DateTime? EnrollmentDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITLA.DAL.Entities
{
    //[Table("Instructor", Schema = "dbo")]
    public class Instructors :Core.Person
    {
        //public int Id { get; set; }
        public DateTime? HireDate { get; set; }
    }
}

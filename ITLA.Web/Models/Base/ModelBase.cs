using System;

namespace ITLA.Web.Models.Base
{
    public class ModelBase
    {
        public string User { get; set; }
        public DateTime Date { get; set; }
        public int? UserDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool Deleted { get; set; }
    }
}

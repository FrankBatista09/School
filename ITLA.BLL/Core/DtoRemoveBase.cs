using System;

namespace ITLA.BLL.Core
{
    public class DtoRemoveBase
    {
        public int Id { get; set; }
        public int? UserDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool Deleted { get; set; }
    }
}

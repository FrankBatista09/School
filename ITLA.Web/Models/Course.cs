namespace ITLA.Web.Models
{
    public class Course : Base.ModelBase
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
    }
}

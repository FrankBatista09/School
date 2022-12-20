using ITLA.BLL.Core;
using ITLA.BLL.Dtos;
using ITLA.BLL.Responses;

namespace ITLA.BLL.Contracts
{
    public interface ICourseService : IBaseService
    {
        CourseSaveResponse SaveCourse(CourseSaveDto courseSaveDto);
        CourseUpdateResponse UpdateCourse(CourseUpdateDto courseSaveDto);
        ServiceResult RemoveCourse(CourseRemoveDto courseRemoveDto);

        CourseResponse GetCourses();
    }
}

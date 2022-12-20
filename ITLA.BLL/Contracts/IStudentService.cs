using ITLA.BLL.Core;
using ITLA.BLL.Dtos;
using ITLA.BLL.Responses;

namespace ITLA.BLL.Contracts
{
    public interface IStudentService : IBaseService
    {
        StudentSaveResponse SaveStudent(StudentSaveDto studentSaveDto);
        StudentUpdateResponse UpdateStudent(StudentUpdateDto studentUpdateDto);
        ServiceResult RemoveStudent(StudentRemoveDto studentRemoveDto);
        ServiceResult GetStudentsGrades();
    }
}

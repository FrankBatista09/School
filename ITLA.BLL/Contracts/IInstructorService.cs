using ITLA.BLL.Core;
using ITLA.BLL.Dtos;
using ITLA.BLL.Responses;

namespace ITLA.BLL.Contracts
{
    public interface IInstructorService : IBaseService
    {
        InstructorSaveResponse SaveInstructor(InstructorSaveDto instructorSaveDto);
        InstructorUpdateResponse UpdateInstructor(InstructorUpdateDto instructorUpdateDto);
        ServiceResult DeleteInstructor(InstructorRemoveDto instructorRemoveDto);

        InstructorResponse GetInstructors();
    }
}

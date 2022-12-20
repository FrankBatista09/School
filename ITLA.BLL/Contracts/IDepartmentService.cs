using ITLA.BLL.Core;
using ITLA.BLL.Dtos;
using ITLA.BLL.Responses;

namespace ITLA.BLL.Contracts
{
    public interface IDepartmentService : IBaseService
    {
        DepartmentSaveResponse SaveDepartment(DepartmentSaveDto departmentSaveDto);
        DepartmentUpdateResponse UpdateDepartment(DepartmentUpdateDto departmentUpdateDto);
        ServiceResult RemoveDepartment(DepartmentRemoveDto departmentRemoveDto);
        ServiceResult GetDepartmentById();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ITLA.BLL.Core
{
    public interface IBaseService
    {
        ServiceResult GetAll();
        ServiceResult GetById(int Id);
    }
}

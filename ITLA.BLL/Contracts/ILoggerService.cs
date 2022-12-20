using System;
using System.Collections.Generic;
using System.Text;

namespace ITLA.BLL.Contracts
{
    public interface ILoggerService<TService> where TService : Core.IBaseService
    {
        void LogError(string message, params object[] args);
        void LogInformation(string message, params object[] args);
        void LogDebug(string message, params object[] args);
        void LogWarning(string message, params object[] args);
    }
}

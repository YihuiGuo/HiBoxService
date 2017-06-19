using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Log
{
    public interface ILogger
    {
        void LogError(string ErrorMsg);
        void LogWarning(string WarningMsg);
        void LogMessage(string Msg);
    }
}

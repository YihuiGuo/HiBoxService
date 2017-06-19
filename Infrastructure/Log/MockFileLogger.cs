using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Log
{
    public class MockFileLogger : ILogger
    {
        string logFileName = "HiBoxServiceLog.txt";
        public void LogError(string ErrorMsg)
        {
            using (var fs = File.AppendText(logFileName))
            {
                fs.WriteLine("[Error]" + DateTime.Now.ToShortTimeString() + ErrorMsg);
            }
        }

        public void LogMessage(string Msg)
        {
            using (var fs = File.AppendText(logFileName))
            {
                fs.WriteLine("[Message]" + DateTime.Now.ToShortTimeString() + Msg);
            }
        }

        public void LogWarning(string WarningMsg)
        {
            using (var fs = File.AppendText(logFileName))
            {
                fs.WriteLine("[Warning]" + DateTime.Now.ToShortTimeString() + WarningMsg);
            }
        }
    }
}

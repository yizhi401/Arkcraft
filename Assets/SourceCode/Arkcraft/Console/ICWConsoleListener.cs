using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arkcraft.Console
{
    public interface ICWConsoleListener
    {
        void Log(CWConsole.LogLevel level, string message);
    }
}

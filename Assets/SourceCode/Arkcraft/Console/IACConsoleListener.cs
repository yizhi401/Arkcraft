using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arkcraft.Console
{
    public interface IACConsoleListener
    {
        void Log(ACConsole.LogLevel level, string message);
    }
}

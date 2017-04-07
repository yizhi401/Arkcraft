using System;
using Arkcraft.World.Lights;

namespace Arkcraft.Configuration
{
    public class ConfigDayInfo
    {
        public string name;

        public int dayDuration;
        public DayTimeLuminanceInfo[] dayTimeLuminances;
    }
}

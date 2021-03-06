using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Fss.Rasp
{
    public abstract class Interface : RaspApi<Interface, Message>
    {
        public static Dictionary<int, string> GetCurrentProcesses()
        {
            return Process.GetProcesses()
                .Select(p => new KeyValuePair<int, string>(p.Id, p.ProcessName))
                .ToDictionary(p => p.Key, p => p.Value);
        }
    }
}

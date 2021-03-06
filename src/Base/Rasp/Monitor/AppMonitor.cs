using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fss.Rasp
{
    public abstract class AppMonitor<TDetector, TDetectorMessage, TMessage> : 
        Monitor<TDetector, TDetectorMessage, TMessage>
        where TDetector : ActivityDetector<TDetectorMessage>
        where TDetectorMessage : Message
        where TMessage : Message
    {
        #region Constructors
        public AppMonitor(Profile profile, string processName) : base(profile)
        {
            var processes = Process.GetProcessesByName(processName);
            if (processes == null || processes.Length == 0)
            {
                Error("No processes to monitor.");
                Status = ApiStatus.ProcessNotFound;
                return;
            }
            else
            {
                Processes = processes;
                ProcessName = processName;
                Status = ApiStatus.Initializing;
            }
        }
        #endregion

        #region Properties
        public string ProcessName { get; protected set; }

        public Process[] Processes { get; protected set; }
        #endregion

        #region Methods
        protected static Process GetProcessById(int id)
        {
            try
            {
                return Process.GetProcessById(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Fss.Rasp
{
    public abstract class ActivityDetector<TMessage>: RaspApi<ActivityDetector<TMessage>, TMessage>, IActivityDetector
        where TMessage : Message
    {
        #region Constructors
        public ActivityDetector(int pid, IMonitor monitor)
        {
            processId = pid;
            Monitor = monitor;
            Global.MessageQueue.Add(this);
        }
        #endregion

        #region Abstract methods
        public abstract ApiResult Enable();
        #endregion

        #region Properties
        public IMonitor Monitor { get; }
        public long CurrentArtifactId => currentMessageId;
        #endregion

        #region Fields
        protected Type monitorType;
        protected int processId;
        #endregion
    }
}

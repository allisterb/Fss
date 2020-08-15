using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fss.Rasp
{
    public interface IActivityDetector : IQueueProducer
    {
        ApiResult Enable();
    }
}

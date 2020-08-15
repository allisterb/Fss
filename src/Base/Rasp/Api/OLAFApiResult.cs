using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fss.Rasp
{
    [Flags]
    public enum ApiResult
    {
        Unknown = -1,
        Success = 0,
        Failure = 1,
        NoOp = 2
    }
}

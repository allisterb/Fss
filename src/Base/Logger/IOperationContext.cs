using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fss
{
    public interface IOperationContext : IDisposable
    {
        void Complete();
        void Cancel();
    }
}

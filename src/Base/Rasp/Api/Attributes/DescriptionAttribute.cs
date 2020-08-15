using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fss.Rasp
{
    [AttributeUsage(AttributeTargets.Class),]
    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string d)
        {
            Description = d;
        }
        public string Description { get; }
    }
    
}

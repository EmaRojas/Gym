using App.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Domain
{
    public class Log : BaseEntity
    {
        public virtual string Action { get; set; }
        public virtual string Message { get; set; }
        public virtual DateTime Created { get; set; }
    }
}

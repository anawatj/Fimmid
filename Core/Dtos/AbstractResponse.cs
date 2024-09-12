using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
  
    public abstract class AbstractResponse
    {
       
        public abstract int Code { get; set; }
    }
}

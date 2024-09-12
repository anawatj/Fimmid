using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    [DataContract]
    public class ResponseSuccess<T> : AbstractResponse
    {
        [DataMember(Name ="code")]
        public override int Code { get; set; }
        [DataMember(Name ="data")]
        public T Data { get; set; }
    }
}

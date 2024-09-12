using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    [DataContract]
    public class ResponseError : AbstractResponse
    {
        [DataMember(Name ="code")]
        public override int Code { get; set; }
        [DataMember(Name ="error")]
        public string Error { get; set; }
    }
}

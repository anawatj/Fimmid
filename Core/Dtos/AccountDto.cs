using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    [DataContract]
    public class AccountDto
    {
        [DataMember(Name ="id")]
        public string Id { get; set; }
        [DataMember(Name ="balance")]
        public decimal? Balance { get; set; }
    }
}

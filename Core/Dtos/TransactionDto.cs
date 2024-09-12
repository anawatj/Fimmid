using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    [DataContract]
    public class TransactionDto
    {
        [DataMember(Name ="id")]
        public string Id { get; set; }
        [DataMember(Name ="fromAccountId")]
        public string FromAccountId { get; set; }
        [DataMember(Name ="toAccountId")]
        public string ToAccountId { get; set; }
        [DataMember(Name ="amount")]
        public decimal? Amount { get; set; }
    }
}

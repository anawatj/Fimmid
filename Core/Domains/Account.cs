using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains
{
    public class Account : AbstractDomain<Guid>
    {
        public decimal? Balance { get; set; }

        public List<Transaction> FromTransactions { get; set; }

        public List<Transaction> ToTransactions { get; set; }
    }
}

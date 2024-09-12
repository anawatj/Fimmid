using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains
{
    public class Transaction : AbstractDomain<Guid>
    {
        public  decimal? Amount { get; set; }
        public  Guid FromAccountId { get; set; }
        public  Account FromAccount { get; set; }
        public  Guid ToAccountId { get; set; }
        public  Account ToAccount { get; set; }
    }
}

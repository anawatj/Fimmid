using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domains;
namespace Core.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction,Guid>
    {
    }
}

using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ITransactionService : IBaseService<TransactionDto>
    {
        public AbstractResponse NewTransaction(TransactionDto data);
        public AbstractResponse FetchAllTransaction();
    }
}

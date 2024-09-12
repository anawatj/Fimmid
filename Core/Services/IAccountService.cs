using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IAccountService :IBaseService<AccountDto>
    {
        public AbstractResponse CreateNewAccount(AccountDto data);
        public AbstractResponse FetchAccountById(string accountId);

        public AbstractResponse FetchAllAccount();
    }
}

using Core.Domains;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implements.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private ApplicationDbContext db;

        public AccountRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Account Create(Account entity)
        {
            this.db.Accounts.Add(entity);
            return entity;
        }

        public void Delete(Guid id)
        {
            var account = this.db.Accounts.Where(t => t.Id == id).First<Account>();
            this.db.Accounts.Remove(account);
        }

        public List<Account> FindAll()
        {
            return this.db.Accounts.ToList<Account>();
        }

        public Account? FindById(Guid id)
        {
            return this.db.Accounts.Where(t => t.Id == id).FirstOrDefault<Account>();
        }

        public Account Update(Account entity)
        {
        
            this.db.Accounts.Update(entity);
            return entity;
        }
    }
}

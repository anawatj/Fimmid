using Core.Domains;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implements.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private ApplicationDbContext db;
        public TransactionRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Transaction Create(Transaction entity)
        {
            this.db.Transactions.Add(entity);
            return entity;
        }

        public void Delete(Guid id)
        {
            var transaction = this.db.Transactions.Where(t => t.Id == id).First<Transaction>();
            this.db.Transactions.Remove(transaction);
        }

        public List<Transaction> FindAll()
        {
            var transactions = this.db.Transactions.ToList<Transaction>();
            return transactions;
        }

        public Transaction? FindById(Guid id)
        {
            var transaction = this.db.Transactions.Where(t => t.Id == id).FirstOrDefault<Transaction>();
            return transaction;
        }

        public Transaction Update(Transaction entity)
        {
            this.db.Transactions.Update(entity);
            return entity;
        }
    }
}

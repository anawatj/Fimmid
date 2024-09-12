using AutoMapper;
using Core.Domains;
using Core.Dtos;
using Core.Repositories;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Implements.Services
{
    public class TransactionService : ITransactionService
    {
        private ApplicationDbContext db;
        private IMapper mapper;
        private IAccountRepository accountRepository;
        private ITransactionRepository transactionRepository;
        public TransactionService(ApplicationDbContext db,IMapper mapper,IAccountRepository accountRepository,ITransactionRepository transactionRepository)
        {
            this.db = db;
            this.mapper = mapper;
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
        }

        public AbstractResponse FetchAllTransaction()
        {
            try
            {
                var transactions = this.transactionRepository.FindAll();
                if (transactions.Count == 0)
                {
                    var error = new ResponseError();
                    error.Code = (int)HttpStatusCode.NotFound;
                    error.Error = "Transaction Not Found";
                    return error;
                }
                var result = mapper.Map<List<Transaction>, List<TransactionDto>>(transactions);
                var res = new ResponseSuccess<List<TransactionDto>>();
                res.Code = (int)HttpStatusCode.OK;
                res.Data = result;
                return res;
            }
            catch(Exception ex)
            {
                var error = new ResponseError();
                error.Code = (int)HttpStatusCode.InternalServerError;
                error.Error = ex.Message;
                return error;
            }
          
        }

        public AbstractResponse NewTransaction(TransactionDto data)
        {
            var errors = Validate(data);
            if (errors.Count > 0)
            {
                var error = new ResponseError();
                error.Code = (int)HttpStatusCode.BadRequest;
                error.Error = string.Join(",", errors);
                return error;
            }
            try
            {
                this.db.Database.BeginTransaction();
                var entity = mapper.Map<TransactionDto, Transaction>(data);

                var fromAccount = accountRepository.FindById(entity.FromAccountId);
                if (fromAccount == null)
                {
                    var error = new ResponseError();
                    error.Code = (int)HttpStatusCode.NotFound;
                    error.Error = "From Account Not Found";
                    return error;
                }
                else
                {
                    if (fromAccount.Balance < entity.Amount)
                    {
                        var error = new ResponseError();
                        error.Code = (int)HttpStatusCode.BadRequest;
                        error.Error = "Cannot Create Transaction";
                        return error;
                    }
                }
                var toAccount = accountRepository.FindById(entity.ToAccountId);
                if (toAccount == null)
                {
                    var error = new ResponseError();
                    error.Code = (int)HttpStatusCode.NotFound;
                    error.Error = "To Account Not Found";
                    return error;
                }

                entity.Id = Guid.NewGuid();
                entity.FromAccount = fromAccount;
                entity.ToAccount = toAccount;
                transactionRepository.Create(entity);
                this.db.SaveChanges();
                fromAccount.Balance = fromAccount.Balance - entity.Amount;
                toAccount.Balance = toAccount.Balance + entity.Amount;
                accountRepository.Update(fromAccount);
                this.db.SaveChanges();
                accountRepository.Update(toAccount);
                this.db.SaveChanges();
                var result = mapper.Map<Transaction, TransactionDto>(entity);
                this.db.Database.CommitTransaction();
                var res = new ResponseSuccess<TransactionDto>();
                res.Code = (int)HttpStatusCode.OK;
                res.Data = result;
                return res;
            }catch(Exception ex)
            {
                this.db.Database.RollbackTransaction();
                var error = new ResponseError();
                error.Code = (int)HttpStatusCode.InternalServerError;
                error.Error = ex.Message;
                return error;
            }
            


        }

        public List<string> Validate(TransactionDto data)
        {
            List<string> errors = new List<string>();
            if (!data.Amount.HasValue)
            {
                errors.Add("Transaction Amount is Required");
            }else
            {
                if (data.Amount <= 0)
                {
                    errors.Add("Transaction Amount is less or equal zero");
                }
            }
            if (string.IsNullOrEmpty(data.FromAccountId))
            {
                errors.Add("From Account Id is Required");
            }else
            {
                Guid fromId;
                Guid.TryParse(data.FromAccountId, out fromId);
                if (fromId == Guid.Empty)
                {
                    errors.Add("From Account Id is invalid format");
                }
            }
            if (string.IsNullOrEmpty(data.ToAccountId))
            {
                errors.Add("To Account Id is Required");
            }
            else
            {
                Guid toId;
                Guid.TryParse(data.ToAccountId, out toId);
                if (toId == Guid.Empty)
                {
                    errors.Add("To Account Id is Invalid format");
                }
            }
            return errors;
        }
    }
}

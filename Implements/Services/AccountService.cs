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
    public class AccountService : IAccountService
    {
        private ApplicationDbContext db;
        private IMapper mapper;
        private IAccountRepository accountRepository;
        public AccountService(ApplicationDbContext db, IMapper mapper, IAccountRepository accountRepository)
        {
            this.db = db;
            this.mapper = mapper;
            this.accountRepository = accountRepository;
        }
        public AbstractResponse CreateNewAccount(AccountDto data)
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
                Account entity = mapper.Map<AccountDto, Account>(data);
                entity.Id = Guid.NewGuid();
                var result = this.accountRepository.Create(entity);
                this.db.SaveChanges();
                var dto = mapper.Map<Account, AccountDto>(result);
                this.db.Database.CommitTransaction();
                var res = new ResponseSuccess<AccountDto>();
                res.Code = (int)HttpStatusCode.Created;
                res.Data = dto;
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

        public AbstractResponse FetchAccountById(string accountId)
        {
           
            try
            {
                Guid id;
                Guid.TryParse(accountId, out id);
                if (id == Guid.Empty)
                {
                    var error = new ResponseError();
                    error.Code = (int)HttpStatusCode.BadRequest;
                    error.Error = "Account Id is Invalid";
                    return error;
                }
                var account = this.accountRepository.FindById(Guid.Parse(accountId));
                if (account == null)
                {
                    var error = new ResponseError();
                    error.Code = (int)HttpStatusCode.NotFound;
                    error.Error = "Account Not Found";
                    return error;
                }
                var result = mapper.Map<Account, AccountDto>(account);
                var res = new ResponseSuccess<AccountDto>();
                res.Code = (int)HttpStatusCode.OK;
                res.Data = result;
                return res;
            }catch(Exception ex)
            {
                var error = new ResponseError();
                error.Code = (int)HttpStatusCode.InternalServerError;
                error.Error = ex.Message;
                return error;
            }
           

        }

        public AbstractResponse FetchAllAccount()
        {
            try
            {
                var accounts = this.accountRepository.FindAll();
                if (accounts.Count == 0)
                {
                    var error = new ResponseError();
                    error.Code = (int)HttpStatusCode.NotFound;
                    error.Error = "Account Not Found";
                    return error;
                }
                var result = mapper.Map<List<Account>, List<AccountDto>>(accounts);
                var res = new ResponseSuccess<List<AccountDto>>();
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

        public List<string> Validate(AccountDto data)
        {
            List<string> errors = new List<string>();
            if (!data.Balance.HasValue)
            {
                errors.Add("Account Balance is Required");
            }
            else
            {
                if (data.Balance <= 0)
                {
                    errors.Add("Account Balance is Not Less than or equal Zero");
                }
            }
            return errors;
        }
    }
}

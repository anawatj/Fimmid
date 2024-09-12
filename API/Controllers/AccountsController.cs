using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountService accountService;
        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet("/api/accounts")]
        public IActionResult GetAllAccounts()
        {
            var res = accountService.FetchAllAccount();
            if(res is ResponseSuccess<List<AccountDto>>)
            {
                return Ok(res);
            }
            else
            {
                var err = (ResponseError)res;
                return Problem(err.Error, null, err.Code, "Error", null);
            }
        }
        [HttpPost("/api/accounts")]
        public IActionResult CreateAccount(AccountDto data)
        {
            var res = accountService.CreateNewAccount(data);
            if(res is ResponseSuccess<AccountDto>)
            {
                return Ok(res);
            }
            else
            {
                var err = (ResponseError)res;
                return Problem(err.Error, null, err.Code, "Error", null);
            }
        }
        [HttpGet("/api/accounts/{accountId}")]
        public IActionResult GetAccountsById(string accountId)
        {
            var res = accountService.FetchAccountById(accountId);
            if(res is ResponseSuccess<AccountDto>)
            {
                return Ok(res);
            }
            else
            {
                var err = (ResponseError)res;
                return Problem(err.Error, null, err.Code, "Error", null);
            }
        }
    }
}

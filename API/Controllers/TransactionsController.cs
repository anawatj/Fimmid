using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private ITransactionService transactionService;
        public TransactionsController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }
        [HttpGet("/api/transactions")]
        public IActionResult GetAllTransactions()
        {
            var res = transactionService.FetchAllTransaction();
            if(res is ResponseSuccess<List<TransactionDto>>)
            {
                return Ok(res);
            }
            else
            {
                var err = (ResponseError)res;
                return Problem(err.Error, null, err.Code, "Error", null);
            }
        }
        [HttpPost("/api/transactions")]
        public IActionResult CreateTransactions(TransactionDto data)
        {
            var res = transactionService.NewTransaction(data);
            if(res is ResponseSuccess<TransactionDto>)
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

using Microsoft.AspNetCore.Mvc;
using YbsSmartCardSystem.Domain.Features.Transaction;
using YbsSmartCardSystem.Domain.Features.Transaction.Models;

namespace YbsSmartCardSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : BaseController
{
    private readonly TransactionService _transactionService;

    public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public IActionResult TransactionList([FromQuery] TransactionListRequestModel request)
    {
        var result = _transactionService.GetList(request);
        return Execute(result);
    }

    [HttpGet("{transactionId:int}")]
    public IActionResult TransactionDetail([FromRoute] TransactionDetailRequestModel request)
    {
        var result = _transactionService.GetById(request);
        return Execute(result);
    }
}

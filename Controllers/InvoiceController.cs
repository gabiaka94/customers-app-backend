using CustomerStoreApi.Models;
using CustomerStoreApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CustomerStoreApi.Controllers;

[EnableCors("MyPolicy")]
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly InvoicesService _invoicesService;

    public InvoicesController(InvoicesService InvoicesService) =>
        _invoicesService = InvoicesService;

    [HttpGet("customer/{id}")]
    public async Task<List<Invoice>> GetByCustomer(string id)
    {
        return await _invoicesService.GetByCustomerId(id);
    }
    

}
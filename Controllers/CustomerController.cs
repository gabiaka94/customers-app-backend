using CustomerStoreApi.Models;
using CustomerStoreApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CustomerStoreApi.Controllers;

[EnableCors("MyPolicy")]
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomersService _customersService;
    private readonly InvoicesService _invoicesService;

    public CustomersController(CustomersService CustomersService, InvoicesService InvoicesService)
    {
        _customersService = CustomersService;
        _invoicesService = InvoicesService;
    }
      

    [HttpGet]
    public async Task<List<CustomerWithInvoiceNumber>> Get()
    {
        List<Customer> customerList = await _customersService.GetAsync();
        List<CustomerWithInvoiceNumber> finalList = new List<CustomerWithInvoiceNumber>();
        foreach(Customer customerEl in customerList){
            CustomerWithInvoiceNumber customerWithInvoiceNumber = new CustomerWithInvoiceNumber();
            customerWithInvoiceNumber.InvoicesCount = customerEl.Id == null ? 0 :  await _invoicesService.GetTotalByCustomer(customerEl.Id);
            customerWithInvoiceNumber.Address = customerEl.Address;
            customerWithInvoiceNumber.Country = customerEl.Country;
            customerWithInvoiceNumber.Id = customerEl.Id;
            customerWithInvoiceNumber.Name = customerEl.Name;
            customerWithInvoiceNumber.State = customerEl.State;
            customerWithInvoiceNumber.Subscription = customerEl.Subscription;
            finalList.Add(customerWithInvoiceNumber);

        }
        return finalList;
    }
      

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> Get(string id)
    {
        var Customer = await _customersService.GetAsync(id);
        if (Customer is null)
        {
            return NotFound();
        }

        return Customer;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Customer newCustomer)
    {
        try
        {
            CustomerValidation(newCustomer);
            await _customersService.CreateAsync(newCustomer);
            return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);
        }
        catch (Exception err)
        {
           return BadRequest(err.Message);
        }
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Customer updatedCustomer)
    {
        var Customer = await _customersService.GetAsync(id);

        if (Customer is null)
        {
            return NotFound();
        }

         try
        {
            CustomerValidation(updatedCustomer);
            //update object with the not updatable values by client.
            updatedCustomer.Id = Customer.Id;
            updatedCustomer.Subscription = Customer.Subscription;
            await _customersService.UpdateAsync(id, updatedCustomer);
        }
        catch (Exception err)
        {
           return BadRequest(err.Message);
        }
        return NoContent();
    }

    private void CustomerValidation( Customer Customer){
        if(Customer.Name.Length < 1)
        {
          throw new Exception("Name Field is Mandatory");
        }
        if(Customer.Address.Length < 1)
        {
           throw new Exception("Customer Field is Mandatory");
        }
        if(Customer.Country.Length < 1)
        {
             throw new Exception("Country Field is Mandatory");
        }
        if(Customer.State.Length < 1)
        {
              throw new Exception("State Field is Mandatory");
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        [HttpGet]
        [Route("all")]
        public IActionResult GetAllInvoices()
        {
            return Ok("all invoices");
        }[HttpPost]
        [Route("all")]
        public IActionResult AllInvoices()
        {
            return Ok("all invoices----");
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetInvoice(string id)
        {
            return Ok($"Invoice with id : {id}");
        }
    }
}

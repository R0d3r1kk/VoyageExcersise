using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VoyageExcercise.DAL;
using VoyageExcercise.DAL.Models;
using VoyageExcercise.Helpers;
using VoyageExcercise.Interfaces;
using VoyageExcercise.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VoyageExcercise.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : Controller
    {
        //Database context
        private readonly AppDBContext _context;
        //Invoice Helper (Logic methods)
        private readonly IInvoice _inv_services;
        //Crud Services Helper (Logic methods)
        private readonly IServices _crud_services;

        /// <summary>
        /// Local Database services Instances
        /// </summary>
        public InvoicesController(AppDBContext context, IInvoice invservices, IServices crudservices)
        {
            this._context = context;
            this._inv_services = invservices;
            this._crud_services = crudservices;
        }

        /// <summary>
        /// Generate all the invoices from db.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// page = 0
        /// pagesize = 10
        /// </remarks>
        /// <param name="page">Number of the page</param>
        /// <param name="pagesize">Number of elements of that page</param>
        /// <returns>A Transactions List</returns>    
        [HttpPost("GenInvoice")]
        public List<Invoice> GenInvoice(int page = 0, int pagesize = 10)
        {
            return _inv_services.GetInvoices(_context, page, pagesize);
        }

        /// <summary>
        /// Get invoice by range
        /// </summary>
        /// <response code="200">Returns an invoice list</response>
        /// <response code="400">If the start date is incorrect or the en date is incorrect</response>
        [HttpPost("GetRangedInvoice")]
        public ActionResult GetRangedInvoice(DateTime start, DateTime end)
        {
            if (start.ToString() == null || end.ToString() == null)
            {
                return BadRequest("the dates musn't be null");
            }

            if(start.ToString() == end.ToString())
            {
                BadRequest("The dates musn't be equal");
            }

            var list = _inv_services.GetRangedInvoice(_context, start, end);

            return Ok(JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// Get an invoice by id
        /// </summary>
        /// <response code="200">Returns the invoice data model</response>
        /// <response code="400">If the transaction id is incorrect</response>
        /// <response code="404">If the transaction doesn't exist</response>
        [HttpPost("GetInvoice")]
        public ActionResult GetInvoice(int transaction_id)
        {
            if (transaction_id <= -1)
            {
                return BadRequest();
            }

            var i = _inv_services.GetInvoice(_context, transaction_id);
            if (i == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(i));
        }


        /// <summary>
        /// An example of a transaction with a generated payment 
        /// </summary>
        /// <response code="200">Returns the transaction data model</response>
        /// <response code="500">If something went wrong making the transaction payment</response>
        /// <returns>An http response with the result of the query</returns>
        [HttpPost("GenTransaction")]
        public async Task<IActionResult> GenerateTransactionWithPayment(PaymentWTransactionRequest request)
        {
            try
            {
                //store of the service payment
                var payment = await _crud_services.AddPayment(_context, new PaymentRequest()
                {
                    amount = request.amount,
                    desc = request.payment_desc,
                    status = request.status,
                    ext_src_response = request.ext_src_response,
                    type = request.type
                });

                //check if the payment was stored
                if (payment != null)
                {
                    //store of the transaction with the payment binded by the id
                    var transaction = await _crud_services.AddTransaction(_context,
                        new TransactionRequest() {
                            date_created = DateTime.Now,
                            date_updated = DateTime.Now,
                            description = request.transaction_desc,
                            service_id = ((int)request.service_id)
                        },
                        payment.payment_id);

                    //check if the transaction was stored
                    if(transaction != null)
                    {
                        return Ok(JsonConvert.SerializeObject(transaction));
                    }

                    return StatusCode(500, "Transaction no registered");
                }
                else
                {
                    return StatusCode(500, "Payment not registered");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Updates a transaction payment
        /// </summary>
        /// <response code="200">Returns the new transaction data model</response>
        /// <response code="500">If something went wrong making the transaction update</response>
        /// <returns>A transaction model updated</returns>
        [HttpPut("UpdateTransaction")]
        public async Task<IActionResult> UpdateTransaction(int transaction_id, PaymentRequest request)
        {
            try
            {
                //get the transaction from db
                var transaction = _crud_services.GetTransaction(_context, transaction_id);
                //check if exists
                if(transaction != null)
                {
                    //edit the payment
                    var p = await _crud_services.EditPayment(_context, request, transaction.payment_id);
                    //check if the payment was edited
                    if (p != null) {
                        //generate an invoice to show the data
                        var i = _inv_services.GetInvoice(_context, transaction_id);
                        if (i != null)
                            return Ok(JsonConvert.SerializeObject(i));
                        else
                            return Ok();
                    }
                    else
                        return StatusCode(500, "Payment not updated");
                }
                else
                {
                    return StatusCode(500, "Transaction doesn't exist");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}

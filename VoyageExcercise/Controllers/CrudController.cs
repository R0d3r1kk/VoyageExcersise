using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VoyageExcercise.DAL;
using VoyageExcercise.Interfaces;
using VoyageExcercise.DAL.Models;
using VoyageExcercise.Models;
using System.Threading.Tasks;

namespace VoyageExcercise.Controllers
{
    /// <summary>
    /// Services Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : Controller
    {
        //Database context
        private readonly AppDBContext _context;
        //services Helper (Logic methods)
        private readonly IServices _services;

        /// <summary>
        /// Local Database services Instances
        /// </summary>
        public CrudController(AppDBContext context, IServices services)
        {
            this._context = context;
            this._services = services;
        }

        #region Transaction Requests

        /// <summary>
        /// Get all the transactions from db.
        /// </summary>
        /// <returns>A Transactions List</returns>    
        [HttpPost("GetAllTransactions")]
        public List<Transactions> GetAllTransactions(int page, int pagesize)
        {
            return _services.GetAllTransactions(_context, page, pagesize);
        }

        /// <summary>
        /// Get a transaction from db.
        /// </summary>
        /// <returns>A Transaction data model</returns>
        /// <response code="200">Returns the transaction data model</response>
        /// <response code="400">If the transaction id is incorrect</response>
        /// <response code="404">If the transaction doesn't exist</response>
        [HttpPost("GetTransaction")]
        public IActionResult GetTransaction(int transaction_id)
        {
            if (transaction_id <= -1)
            {
                return BadRequest();
            }

            var t = _services.GetTransaction(_context, transaction_id);

            if(t == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(t));
        }

        /// <summary>
        /// Add a transaction to the db.
        /// </summary>
        /// <returns>A Transaction data model</returns>
        /// <response code="201">If the transaction was created returns the transaction data model</response>
        /// <response code="304">If the transaction was not created</response>
        [HttpPost("AddTransaction")]
        public async Task<IActionResult> AddTransaction(TransactionRequest request, int transaction_id)
        {
            var t = await _services.AddTransaction(_context, request, transaction_id);

            if (t != null)
                return Created("Transaction", JsonConvert.SerializeObject(t));
            else
                return StatusCode(304);//Not Modified
               
        }

        /// <summary>
        /// Edit a transaction from db.
        /// </summary>
        /// <returns>A Transaction data model</returns>
        /// <response code="201">If the transaction was edite returns the transaction data model</response>
        /// <response code="304">If the transaction was not edited</response>
        [HttpPut("EditTransaction")]
        public async Task<IActionResult> EditTransaction(TransactionRequest request, int transaction_id)
        {
            var t = await _services.EditTransaction(_context, request, transaction_id);

            if (t != null)
                return Ok(JsonConvert.SerializeObject(t));
            else
                return StatusCode(304);//Not Modified

        }

        /// <summary>
        /// Delete a transaction from db.
        /// </summary>
        /// <returns>A Transaction data model</returns>
        /// <response code="201">If the transaction was deleted returns the transaction data model</response>
        /// <response code="304">If the transaction was not deleted</response>
        [HttpDelete("DeleteTransaction")]
        public async Task<IActionResult> DeleteTransaction(int transaction_id)
        {
            var t = await _services.DeleteTransaction(_context, transaction_id);

            if (t)
                return Ok(t);
            else
                return StatusCode(304);//Not Modified

        }

        #endregion

        #region Payment Requests

        /// <summary>
        /// Get all the payments from db.
        /// </summary>
        /// <returns>A Payment List</returns>    
        [HttpPost("GetAllPayments")]
        public List<Payments> GetAllPayments(int page, int pagesize)
        { 
            return _services.GetAllPayments(_context, page, pagesize);
        }

        /// <summary>
        /// Get a payment from db.
        /// </summary>
        /// <returns>A Payment data model</returns>
        /// <response code="200">Returns the payment data model</response>
        /// <response code="400">If the payment id is incorrect</response>
        /// <response code="404">If the payment doesn't exist</response>
        [HttpPost("GetPayment")]
        public IActionResult GetPayment(int payment_id)
        {
            if (payment_id <= -1)
            {
                return BadRequest();
            }

            var p = _services.GetPayment(_context, payment_id);
            if (p == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(p));
        }

        /// <summary>
        /// Add a payment to the db.
        /// </summary>
        /// <returns>A Payment data model</returns>
        /// <response code="201">If the payment was created returns the payment data model</response>
        /// <response code="304">If the payment was not created</response>
        [HttpPost("AddPayment")]
        public async Task<IActionResult> AddPayment(PaymentRequest request)
        {
            var p = await _services.AddPayment(_context, request);

            if (p != null)
                return Created("Payment", JsonConvert.SerializeObject(p));
            else
                return StatusCode(304);//Not Modified

        }

        /// <summary>
        /// Edit a payment from db.
        /// </summary>
        /// <returns>A Payment data model</returns>
        /// <response code="201">If the payment was edited returns true</response>
        /// <response code="304">If the payment was not edited</response>
        [HttpPut("EditPayment")]
        public async Task<IActionResult> EditPayment(PaymentRequest request, int payment_id)
        {
            var p = await _services.EditPayment(_context, request, payment_id);

            if (p!= null)
                return Ok(JsonConvert.SerializeObject(p));
            else
                return StatusCode(304);//Not Modified

        }

        /// <summary>
        /// Delete a payment from db.
        /// </summary>
        /// <returns>A Payment data model</returns>
        /// <response code="201">If the payment was deleted returns true</response>
        /// <response code="304">If the payment was not deleted</response>
        [HttpDelete("DeletePayment")]
        public async Task<IActionResult> DeletePayment(int payment_id)
        {
            var p = await _services.DeletePayment(_context, payment_id);

            if (p)
                return Ok(p);
            else
                return StatusCode(304);//Not Modified

        }

        #endregion

        #region Service Requests

        /// <summary>
        /// Get all the services from db.
        /// </summary>
        /// <returns>A Service List</returns>    
        [HttpPost("GetAllServices")]
        public List<CServices> GetAllServices(int page, int pagesize)
        {
            return _services.GetAllServices(_context, page, pagesize);
        }

        /// <summary>
        /// Get a service from db.
        /// </summary>
        /// <returns>A Service data model</returns>
        /// <response code="200">Returns the service data model</response>
        /// <response code="400">If the service id is incorrect</response>
        /// <response code="404">If the service doesn't exist</response>
        [HttpPost("GetService")]
        public IActionResult GetService(int service_id)
        {
            if (service_id <= -1)
            {
                return BadRequest();
            }

            var p = _services.GetService(_context, service_id);
            if (p == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(p));
        }

        /// <summary>
        /// Add a service to the db.
        /// </summary>
        /// <returns>A Service data model</returns>
        /// <response code="201">If the service was created returns the service data model</response>
        /// <response code="304">If the service was not created</response>
        [HttpPost("AddService")]
        public async Task<IActionResult> addService(string service_name)
        {
            var s = await _services.AddService(_context, service_name);

            if (s != null)
                return Created("Service", JsonConvert.SerializeObject(s));
            else
                return StatusCode(304);//Not Modified

        }

        /// <summary>
        /// Edit a service from db.
        /// </summary>
        /// <returns>A Service data model</returns>
        /// <response code="201">If the service was edited returns true</response>
        /// <response code="304">If the service was not edited</response>
        [HttpPut("EditService")]
        public async Task<IActionResult> EditService(string service_name, int service_id)
        {
            var s = await _services.EditService(_context, service_name, service_id);

            if (s != null)
                return Ok(JsonConvert.SerializeObject(s));
            else
                return StatusCode(304);//Not Modified

        }

        /// <summary>
        /// Delete a service from db.
        /// </summary>
        /// <returns>A Service data model</returns>
        /// <response code="201">If the service was service returns true</response>
        /// <response code="304">If the service was not deleted</response>
        [HttpDelete("DeleteService")]
        public async Task<IActionResult> DeleteService(int service_id)
        {
            var s = await _services.DeleteService(_context, service_id);

            if (s)
                return Ok(s);
            else
                return StatusCode(304);//Not Modified

        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VoyageExcercise.DAL;
using VoyageExcercise.DAL.Models;
using VoyageExcercise.Interfaces;

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
        /// <returns>A Transactions List</returns>    
        [HttpPost("GenInvoice")]
        public List<Invoice> GenInvoice(int page, int pagesize)
        {
            return _inv_services.GetInvoices(_context, page, pagesize);
        }

    }
}

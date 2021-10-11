using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoyageExcercise.DAL;
using VoyageExcercise.DAL.Models;

namespace VoyageExcercise.Interfaces
{
    /// <summary>
    /// Invoice interface
    /// </summary>
    public interface IInvoice
    {
        /// <summary>
        /// Method <c>GetInvoices</c> Get a list of the invoices from db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <returns>An Invoce list</returns>
        public List<Invoice> GetInvoices(AppDBContext context, int page, int pagesize);
        /// <summary>
        /// Method <c>GetInvoice</c> Get an invoice by id.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <returns>An Invoce data model</returns>
        public Invoice GetInvoice(AppDBContext context, int transaction_id);

        /// <summary>
        /// Method <c>GetRangedInvoice</c> Get an invoice by a range of dates.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="start_date">the start date you want to query</param>
        /// <param name="start_date">the end date you want to query</param>
        /// <returns>An Invoce list</returns>
        public List<Invoice> GetRangedInvoice(AppDBContext context, DateTime start_date, DateTime end_date);
    }
}

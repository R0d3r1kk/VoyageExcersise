using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoyageExcercise.DAL;
using VoyageExcercise.DAL.Models;
using VoyageExcercise.Interfaces;

namespace VoyageExcercise.Helpers
{
    public class InvoiceHelper: IInvoice
    {

        /// <summary>
        /// Method <c>GetInvoices</c> Get a list of the invoices from db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="page">Number of the page</param>
        /// <param name="pagesize">Number of elements of that page</param>
        /// <returns>An Invoce list</returns>
        public List<Invoice> GetInvoices(AppDBContext context, int page, int pagesize)
        {
            try
            {
                var invoices = context.Invoice.Skip(page * pagesize).Take(pagesize).ToList();
                return invoices;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Invoice>();
            }
        }

        /// <summary>
        /// Method <c>GetInvoice</c> Get an invoice by id.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="transaction_id">Transaction id</param>
        /// <returns>An Invoce data model</returns>
        public Invoice GetInvoice(AppDBContext context, int transaction_id)
        {
            try
            {
                var invoice = context.Invoice.FirstOrDefault(i => i.id == transaction_id);
                return invoice;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Method <c>GetRangedInvoice</c> Get an invoice by a range of dates.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="start_date">the start date you want to query</param>
        /// <param name="start_date">the end date you want to query</param>
        /// <returns>An Invoce list</returns>
        public List<Invoice> GetRangedInvoice(AppDBContext context, DateTime start_date, DateTime end_date)
        {
            try
            {
                var invoices = context.Invoice.Where(i => DateTime.Parse(i.date) >= start_date && DateTime.Parse(i.date) <= end_date).ToList();
                return invoices;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}

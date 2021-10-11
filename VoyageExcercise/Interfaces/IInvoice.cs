using System;
using System.Collections.Generic;
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
    }
}

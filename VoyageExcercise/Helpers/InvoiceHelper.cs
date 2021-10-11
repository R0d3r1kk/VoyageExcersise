using System;
using System.Collections.Generic;
using System.Linq;
using VoyageExcercise.DAL;
using VoyageExcercise.DAL.Models;
using VoyageExcercise.Interfaces;

namespace VoyageExcercise.Helpers
{
    public class InvoiceHelper: IInvoice
    {
        public InvoiceHelper()
        {
        }

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
    }
}

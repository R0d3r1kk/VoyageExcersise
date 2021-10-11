using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoyageExcercise.DAL;
using VoyageExcercise.DAL.Models;
using VoyageExcercise.Interfaces;
using VoyageExcercise.Models;

namespace VoyageExcercise.Helpers
{
    /// <summary>
    /// Services methods
    /// </summary>
    public class ServicesHelper: IServices
    {
        #region Transactions Methods

        /// <summary>
        /// Method <c>AddTransaction</c> Store a transaction in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="request">TransactionRequest data model</param>
        /// <param name="payment_id">The Payment Id</param>
        /// <returns>true if the transaction was added, false if the update fails</returns>
        public async Task<Transactions> AddTransaction(AppDBContext context, TransactionRequest request, int payment_id)
        {
            try
            {
                var t = context.Transactions.Add(new Transactions()
                {
                    date_created = request.date_created,
                    date_updated = request.date_updated,
                    description = request.description,
                    payment_id = payment_id,
                    service_id = request.service_id
                });

                await context.SaveChangesAsync();
                return t.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Method <c>DeleteTransaction</c> Delete a transaction stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="transaction_id">The Transaction ID yout want to delete</param>
        /// <returns>true if the transaction was deleted, false if the delete fails</returns>
        public async Task<bool> DeleteTransaction(AppDBContext context, int transaction_id)
        {
            try {
                var transaction = GetTransaction(context, transaction_id);

                if (transaction != null)
                {
                    context.Transactions.Remove(transaction);
                    await context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Method <c>EditTransaction</c> Update a transaction stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="request">TransactionRequest data model with the new data</param>
        /// <param name="transaction_id">The Transaction ID yout want to edit</param>
        /// <returns> true if the transaction was saved, false if the update fails</returns>
        public async Task<Transactions> EditTransaction(AppDBContext context, TransactionRequest request, int transaction_id)
        {
            try
            {
                if (request == null || transaction_id < 0)
                    return null;

                //check if the transaction exist
                var t = GetTransaction(context, transaction_id);
                if (t != null)
                {
                    t.date_updated = request.date_updated;
                    t.description = request.description != t.description ? request.description : t.description;
                    t.service_id = request.service_id != t.service_id ? request.service_id : t.service_id;

                    context.Transactions.Update(t);
                    await context.SaveChangesAsync();

                    return t;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Method <c>GetAllTransactions</c> Get a list of the transactions raw data stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="page">Number of the page</param>
        /// <param name="pagesize">Number of elements of that page</param>
        /// <returns>A Transactions list</returns>
        public List<Transactions> GetAllTransactions(AppDBContext context, int page, int pagesize)
        {
            try
            {
                var transactions = context.Transactions.Skip(page*pagesize).Take(pagesize).ToList();
                return transactions;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Transactions>();
            }
        }

        /// <summary>
        /// Method <c>GetTransaction</c> Get an specific transaction stored in db
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="transaction_id">The Transaction Id you want to get</param>
        /// <returns>Transaction data model</returns>
        public Transactions GetTransaction(AppDBContext context, int transaction_id)
        {
            try { 
                var transaction = context.Transactions.FirstOrDefault(t => t.transaction_id == transaction_id);
                return transaction;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        #endregion

        #region Payments Methods

        /// <summary>
        /// Method <c>GetAllPayments</c> Get a list of the payments data stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="page">Number of the page</param>
        /// <param name="pagesize">Number of elements of that page</param>
        /// <returns>A Payment list</returns>
        public List<Payments> GetAllPayments(AppDBContext context, int page, int pagesize)
            {
                try
                {
                    var payment = context.Payments.Skip(page * pagesize).Take(pagesize).ToList();
                return payment;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new List<Payments>();
                }
            }

            /// <summary>
            /// Method <c>GetPayment</c> Get an specific payment stored in db
            /// </summary>
            /// <param name="context">Application Database Context</param>
            /// <param name="payment_id">The Transaction Id you want to get</param>
            /// <returns>Payment data model</returns>
            public Payments GetPayment(AppDBContext context, int payment_id)
            {
                try
                {
                    var payment = context.Payments.FirstOrDefault(p => p.payment_id == payment_id) as Payments;
                    return payment;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            /// <summary>
            /// Method <c>AddPayment</c> Store a payment in the db.
            /// </summary>
            /// <param name="context">Application Database Context</param>
            /// <param name="request">PaymentRequest data model</param>
            /// <returns> true if the payment was added, false if the update fails</returns>
            public async Task<Payments> AddPayment(AppDBContext context, PaymentRequest request)
            {
                try
                {
                    var p = context.Payments.Add(new Payments()
                    {
                        payment_amount = request.amount,
                        payment_desc = request.desc,
                        payment_status = request.status,//nilled, un-billed, paid
                        payment_type = request.type,
                        payment_ext_src_response = request.ext_src_response
                    });

                    await context.SaveChangesAsync();
                    return p.Entity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            /// <summary>
            /// Method <c>EditPayment</c> Update a payment stored in the db.
            /// </summary>
            /// <param name="context">Application Database Context</param>
            /// <param name="request">PaymentRequest data model with the new data</param>
            /// <param name="payment_id">The payment ID yout want to edit</param>
            /// <returns> true if the payment was saved, false if the update fails</returns>
            public async Task<Payments> EditPayment(AppDBContext context, PaymentRequest request, int payment_id)
            {
                try
                {
                if (request == null || payment_id < 0)
                    return null;

                    //check if the payment exist
                    var p = GetPayment(context, payment_id);
                    if (p != null)
                    {
                        p.payment_amount = request.amount != p.payment_amount ? request.amount : p.payment_amount;
                        p.payment_desc = request.desc != p.payment_desc ? request.desc : p.payment_desc;
                        p.payment_status = request.status != p.payment_status ? request.status : p.payment_status;
                        p.payment_type = request.type != p.payment_type ? request.type : p.payment_type;
                        p.payment_ext_src_response = request.ext_src_response != p.payment_ext_src_response ? request.ext_src_response : p.payment_ext_src_response;

                        context.Payments.Update(p);
                        await context.SaveChangesAsync();


                        return p;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            /// <summary>
            /// Method <c>DeletePayment</c> Delete a payment stored in the db.
            /// </summary>
            /// <param name="context">Application Database Context</param>
            /// <param name="payment_id">The Payment ID yout want to delete</param>
            /// <returns> true if the payment was deleted, false if the delete fails</returns>
            public async Task<bool> DeletePayment(AppDBContext context, int payment_id)
            {
                try
                {
                    var payment = GetPayment(context, payment_id);

                    if (payment != null)
                    {
                        context.Payments.Remove(payment);
                        await context.SaveChangesAsync();
                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

        #endregion

        #region Services Methods
        /// <summary>
        /// Method <c>GetAllServices</c> Get a list of the services data stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="page">Number of the page</param>
        /// <param name="pagesize">Number of elements of that page</param>
        /// <returns>A Services list</returns>
        public List<CServices> GetAllServices(AppDBContext context, int page, int pagesize)
        {
            try
            {
                var services = context.CServices.Skip(page * pagesize).Take(pagesize).ToList();
                return services;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CServices>();
            }
        }
        /// <summary>
        /// Method <c>GetService</c> Get an specific service stored in db
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="service_id">The Transaction Id you want to get</param>
        /// <returns>Payment data model</returns>
        public CServices GetService(AppDBContext context, int service_id)
        {
            try
            {
                var service = context.CServices.FirstOrDefault(s => s.service_id == service_id);
                return service;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Method <c>AddService</c> Store a service in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="service_name">name of the service you want to bind</param>
        /// <returns> true if the service was added, false if the update fails</returns>
        public async Task<CServices> AddService(AppDBContext context, string service_name)
        {
            try
            {
                var s = context.CServices.Add(new CServices()
                {
                    service_name = service_name
                });

                await context.SaveChangesAsync();
                return s.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Method <c>EditService</c> Update a service stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="service_name">The new service name</param>
        /// <param name="service_id">The service ID yout want to edit</param>
        /// <returns> true if the service was saved, false if the update fails</returns>
        public async Task<CServices> EditService(AppDBContext context, string service_name, int service_id)
        {
            try
            {
                //check if the transaction exist
                var s = GetService(context, service_id);
                if (s != null)
                {

                    s.service_name = service_name;

                    context.CServices.Update(s);
                    await context.SaveChangesAsync();


                    return s;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Method <c>DeleteService</c> Delete a service stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="service_id">The service ID yout want to delete</param>
        /// <returns> true if the service was deleted, false if the delete fails</returns>
        public async Task<bool> DeleteService(AppDBContext context, int service_id)
        {
            try
            {
                var service = GetService(context, service_id);

                if (service != null)
                {
                    context.CServices.Remove(service);
                    await context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion
    }
}

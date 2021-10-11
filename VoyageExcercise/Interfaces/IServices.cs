using System.Collections.Generic;
using System.Threading.Tasks;
using VoyageExcercise.DAL;
using VoyageExcercise.DAL.Models;
using VoyageExcercise.Models;

namespace VoyageExcercise.Interfaces
{
    /// <summary>
    /// Service Interface
    /// </summary>
    public interface IServices
    {
        #region Transaction Mehtod References
        /// <summary>
        /// Method <c>GetAllTransactions</c> Get a list of the transactions raw data stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <returns>A Transaction list</returns>
        public List<Transactions> GetAllTransactions(AppDBContext context, int page, int pagesize);
        /// <summary>
        /// Method <c>GetTransaction</c> Get an specific transaction stored in db
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="transaction_id">The Transaction Id you want to get</param>
        /// <returns>Transaction data model</returns>
        public Transactions GetTransaction(AppDBContext context,int transaction_id);
        /// <summary>
        /// Method <c>AddTransaction</c> Store a transaction in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="request">TransactionRequest data model</param>
        /// <param name="payment_id">The Payment Id</param>
        /// <returns> true if the transaction was added, false if the update fails</returns>
        public Task<Transactions> AddTransaction(AppDBContext context, TransactionRequest request, int payment_id);
        /// <summary>
        /// Method <c>EditTransaction</c> Update a transaction stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="request">TransactionRequest data model with the new data</param>
        /// <param name="transaction_id">The Transaction ID yout want to edit</param>
        /// <returns> true if the transaction was saved, false if the update fails</returns>
        public Task<Transactions> EditTransaction(AppDBContext context, TransactionRequest request, int transaction_id);
        /// <summary>
        /// Method <c>DeleteTransaction</c> Delete a transaction stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="transaction_id">The Transaction ID yout want to delete</param>
        /// <returns> true if the transaction was deleted, false if the delete fails</returns>
        public Task<bool> DeleteTransaction(AppDBContext context, int transaction_id);
        #endregion

        #region Payment Mehtod References
        /// <summary>
        /// Method <c>GetAllPayments</c> Get a list of the payments data stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <returns>A Payment list</returns>
        public List<Payments> GetAllPayments(AppDBContext context, int page, int pagesize);
        /// <summary>
        /// Method <c>GetPayment</c> Get an specific payment stored in db
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="payment_id">The Payment Id you want to get</param>
        /// <returns>Payment data model</returns>
        public Payments GetPayment(AppDBContext context, int payment_id);
        /// <summary>
        /// Method <c>AddPayment</c> Store a payment in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="request">PaymentRequest data model</param>
        /// <returns> true if the payment was added, false if the update fails</returns>
        public Task<Payments> AddPayment(AppDBContext context, PaymentRequest request);
        /// <summary>
        /// Method <c>EditPayment</c> Update a payment stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="request">PaymentRequest data model with the new data</param>
        /// <param name="payment_id">The payment ID yout want to edit</param>
        /// <returns> true if the payment was saved, false if the update fails</returns>
        public Task<Payments> EditPayment(AppDBContext context, PaymentRequest request, int payment_id);
        /// <summary>
        /// Method <c>DeletePayment</c> Delete a payment stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="payment_id">The Payment ID yout want to delete</param>
        /// <returns> true if the payment was deleted, false if the delete fails</returns>
        public Task<bool> DeletePayment(AppDBContext context, int payment_id);
        #endregion

        #region Services Mehtod References
        /// <summary>
        /// Method <c>GetAllServices</c> Get a list of the services data stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <returns>A Services list</returns>
        public List<CServices> GetAllServices(AppDBContext context, int page, int pagesize);
        /// <summary>
        /// Method <c>GetService</c> Get an specific service stored in db
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="service_id">The Transaction Id you want to get</param>
        /// <returns>Payment data model</returns>
        public CServices GetService(AppDBContext context, int service_id);
        /// <summary>
        /// Method <c>AddService</c> Store a service in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="service_name">name of the service you want to bind</param>
        /// <returns> true if the service was added, false if the update fails</returns>
        public Task<CServices> AddService(AppDBContext context, string service_name);
        /// <summary>
        /// Method <c>EditService</c> Update a service stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="service_name">The new service name</param>
        /// <param name="service_id">The service ID yout want to edit</param>
        /// <returns> true if the service was saved, false if the update fails</returns>
        public Task<CServices> EditService(AppDBContext context, string service_name, int service_id);
        /// <summary>
        /// Method <c>DeleteService</c> Delete a service stored in the db.
        /// </summary>
        /// <param name="context">Application Database Context</param>
        /// <param name="service_id">The service ID yout want to delete</param>
        /// <returns> true if the service was deleted, false if the delete fails</returns>
        public Task<bool> DeleteService(AppDBContext context, int service_id);
        #endregion
    }
}

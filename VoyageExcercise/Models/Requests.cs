using System;
using VoyageExcercise.Helpers;

namespace VoyageExcercise.Models
{
    /// <summary>
    /// Transaction Request Model
    /// </summary>
    public class TransactionRequest
    {
        public string description { get; set; }
        public int service_id { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_updated { get; set; }
    }

    /// <summary>
    /// Payment Request Model
    /// </summary>
    public class PaymentRequest
    {
        public float amount { get; set; }

        public string desc { get; set; }

        public string status { get; set; }

        public string type { get; set; }

        public string ext_src_response { get; set; }
    }

    /// <summary>
    /// An example of a transaction with a payment in one model
    /// </summary>
    public class PaymentWTransactionRequest
    {
        public EnumService service_id { get; set; }

        public float amount { get; set; }

        public string payment_desc { get; set; }

        public string transaction_desc { get; set; }

        public string status { get; set; }

        public string type { get; set; }

        public string ext_src_response { get; set; }
    }

    /// <summary>
    /// User Request Model
    /// </summary>
    public class UserRequest
    {
        public string name { get; set; }
        public string account { get; set; }
        public string password { get; set; }
    }

    /// <summary>
    /// Login Request Model
    /// </summary>
    public class LoginRequest
    {
        public string account { get; set; }
        public string password { get; set; }
        public string token { get; set; }
    }
}

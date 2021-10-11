using System;
namespace VoyageExcercise.Models
{
    /// <summary>
    /// Transaction Request Model
    /// </summary>
    public class TransactionRequest
    {
        public string description { get; set; }
        public int service_id { get; set; }
        public string date_created
        {
            get
            {
                if(string.IsNullOrEmpty(date_created))
                {
                    return DateTime.Now.ToLongDateString();
                }

                return date_created;
            }
            set {
                date_created = value;
            }
        }
        public string date_updated
        {
            get
            {
                if (string.IsNullOrEmpty(date_updated))
                {
                    return DateTime.Now.ToLongDateString();
                }

                return date_updated;
            }
            set
            {
                date_updated = value;
            }
        }
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
}

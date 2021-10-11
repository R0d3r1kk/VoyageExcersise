using System;
namespace VoyageExcercise.DAL.Models
{
    public class Invoice
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public float amount { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string service { get; set; }
    }
}

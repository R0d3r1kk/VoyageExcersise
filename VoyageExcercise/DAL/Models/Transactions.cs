using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoyageExcercise.DAL.Models
{
    /// <summary>
    /// Transactions Model
    /// </summary>
    [Table("Transactions")]
    public class Transactions
    {
        [Key]
        [Required]
        public int transaction_id { get; set; }

        [Required]
        public DateTime date_created { get; set; }

        [Required]
        public DateTime date_updated { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        public int payment_id{ get; set; }

        public int service_id { get; set; }
    }
}

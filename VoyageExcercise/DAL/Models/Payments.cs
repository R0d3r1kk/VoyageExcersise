using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoyageExcercise.DAL.Models
{
    /// <summary>
    /// Payments Model
    /// </summary>
    [Table("Payments")]
    public class Payments
    {
        [Key]
        [Required]
        public int payment_id { get; set; }


        public float payment_amount { get; set; }


        [StringLength(30)]
        public string payment_desc { get; set; }

        [StringLength(10)]
        public string payment_status { get; set; }

        [StringLength(10)]
        public string payment_type { get; set; }

        public string payment_ext_src_response { get; set; }
        
    }
}

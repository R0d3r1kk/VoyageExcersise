using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoyageExcercise.DAL.Models
{
    /// <summary>
    /// CService Model
    /// </summary>
    [Table("CServices")]
    public class CServices
    {   
        [Key]
        [Required]
        public int service_id { get; set; }


        [StringLength(20)]
        public string service_name { get; set; }
        
    }
}

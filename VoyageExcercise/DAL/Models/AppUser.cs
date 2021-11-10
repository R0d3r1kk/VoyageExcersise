using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VoyageExcercise.DAL.Models
{
    [Table("AppUser")]
    public class AppUser
    {
        [Key]
        [Required]
        public int id { get; set; }

        [StringLength(30)]
        public string name { get; set; }
        [StringLength(30)]
        public string account { get; set; }
        [StringLength(20)]
        public string password { get; set; }

        public DateTime date_created { get; set; }
    }
}

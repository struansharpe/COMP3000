using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITest.Models
{
    public partial class Users
    {
        [Key]
        [StringLength(16)]
        public string Username { get; set; }
        [Required]
        [Column("First_name")]
        [StringLength(16)]
        public string FirstName { get; set; }
        [Required]
        [Column("Last_name")]
        [StringLength(16)]
        public string LastName { get; set; }
        [Required]
        [StringLength(16)]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex2017.Models
{
    [Table("Representative")]
    public class Representative
    {
        [Key]
        [Required]
        public int repID { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string repFirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string repLastName { get; set; }
        [Required]
        [DisplayName("Phone Number")]
        public string repPhoneNumber { get; set; }
        [Required]
        [DisplayName("Email")]
        public string repEmail { get; set; }
        [Required]
        [DisplayName("UserName")]
        public string repUserName { get; set; }
        [Required]
        [DisplayName("Password")]
        public string repPasswordHash { get; set; }
        [Required]
        public int clientID { get; set; }
    }
}
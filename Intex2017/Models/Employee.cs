using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex2017.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int empID { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string empFirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string empLastName { get; set; }
        [Required]
        [DisplayName("Employee SSN")]
        public string empSSNHash { get; set; }
        [Required]
        [DisplayName("Wage")]
        public decimal empWage { get; set; }
        [Required]
        [DisplayName("Email")]
        public string empEmail { get; set; }
        [Required]
        [DisplayName("Password")]
        public string empPassword { get; set; }
    }
}
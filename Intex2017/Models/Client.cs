using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Intex2017.Models
{
    public class Client
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int clientID { get; set; }
        public string clientName { get; set; }
        public string clientPhone { get; set; }
        public string clientAddress1 { get; set; }
        public string clientAddress2 { get; set; }
        public string clientCity { get; set; }
        public string clientState { get; set; }
        public string clientZipcode { get; set; }
        public decimal clientDiscountPct { get; set; }
        public string clientCreationDate { get; set; }
        public decimal clientRunningTotal { get; set; }
    }
}
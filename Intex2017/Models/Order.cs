using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex2017.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int orderNumber { get; set; }
        [Required]
        public string orderDate { get; set; }
        [Required]
        public int orderStatusID { get; set; }
        [Required]
        public string orderDateDue { get; set; }
        [Required]
        public decimal orderPctCompletion { get; set; }
        [Required]
        public string orderCustomerComment { get; set; }
        [Required]
        public int orderDeliveryPaper { get; set; }
        [Required]
        public int orderDeliveryElectronic { get; set; }
        [Required]
        public decimal orderAdvancePayment { get; set; }
        [Required]
        public int cleintID { get; set; }
    }
}
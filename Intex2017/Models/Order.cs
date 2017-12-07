using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex2017.Models
{
    
    public class Order
    {
        [Key]
        public int orderNumber { get; set; }
        [Required]
        [DisplayName("Date Placed")]
        public string orderDate { get; set; }
        [Required]
        [DisplayName("Status")]
        public string orderStatusDesc { get; set; }
        [Required]
        [DisplayName("Due Date")]
        public string orderDateDue { get; set; }
        [Required]
        [DisplayName("% Completed")]
        public decimal orderPctCompletion { get; set; }
        [Required]
        [DisplayName("Comments")]
        public string orderCustomerComment { get; set; }
        [Required]
        [DisplayName("Paper Delivery")]
        public string orderDeliveryPaper { get; set; }
        [Required]
        [DisplayName("Electronic Delivery")]
        public string orderDeliveryElectronic { get; set; }
        [Required]
        [DisplayName("Advanced Payment")]
        public decimal orderAdvancePayment { get; set; }
        [Required]
        public int clientID { get; set; }
    }
}
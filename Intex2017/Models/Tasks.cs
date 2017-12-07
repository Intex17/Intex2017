using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intex2017.Models
{
    public class Tasks
    {
        [Key]
        public int taskID { get; set; }
        [Required]
        [DisplayName("Task Name")]
        public string taskName { get; set; }
        [Required]
        [DisplayName("Task Desc")]
        public string taskDesc { get; set; }
        [Required]
        [DisplayName("Tube Number")]
        public int tubeNumber { get; set; }
        [Required]
        [DisplayName("Employee ID")]
        public int empID { get; set; }

    }
}
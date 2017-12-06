using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Intex2017.Models
{
    public class ClientLink
    {

        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Representative> Representative { get; set; }
        public IEnumerable<Order> Order { get; set; }

    }
}
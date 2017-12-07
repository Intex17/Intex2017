using Intex2017.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace Intex2017.DAL
{
    public class IntexContext : DbContext
    {
        public IntexContext() : base("IntexContext")
        {

        }

        public DbSet<Representative> Representatives { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }

        public System.Data.Entity.DbSet<Intex2017.Models.Tasks> Tasks { get; set; }
    }
}
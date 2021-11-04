using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MarketSqlContext : DbContext
    {
        public MarketSqlContext() : base ("SqlDbConnection") { }

        public DbSet<Customer> Customers { get; set; }
    }
}

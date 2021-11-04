using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MarketOleDbContext : DbContext
    {
        public MarketOleDbContext() : base("OleDbConnection") { }

        public DbSet<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Migrations;

namespace Models.Migrations
{
    public class OleDbMigrationConfiguration: DbMigrationsConfiguration<MarketOleDbContext>
    {
        public OleDbMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}

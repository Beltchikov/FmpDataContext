using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace FmpDataContext
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DbContext>
    {
        public DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FmpData;Integrated Security=True;");

            return new DbContext(optionsBuilder.Options);
        }
    }
}

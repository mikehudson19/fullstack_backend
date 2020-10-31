
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FullStack.Data
{
    public class FullStackContextFactory : IDesignTimeDbContextFactory<FullStackDbContext>
    {

        public FullStackDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FullStackDbContext>();
            optionsBuilder.UseSqlite("Data Source=FullstackDb.db");

            return new FullStackDbContext(optionsBuilder.Options);
        }
    }
}
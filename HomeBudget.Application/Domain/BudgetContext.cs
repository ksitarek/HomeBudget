using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Application.Domain
{
    public class BudgetContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
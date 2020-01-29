using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WgSystems.Models;

namespace WgSystems.Data
{
    public class WgSystemsDbContext : DbContext
    {
        public WgSystemsDbContext(DbContextOptions<WgSystemsDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_SVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog_SVC.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CatalogItem> Items { get; set; }
    }
}
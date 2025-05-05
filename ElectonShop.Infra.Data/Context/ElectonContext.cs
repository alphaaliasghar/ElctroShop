using ElectonShop.Domain.Models.Order;
using ElectonShop.Domain.Models.Product;
using ElectonShop.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Infra.Data.Context
{
    public class ElectonContext : DbContext
    {
        public ElectonContext(DbContextOptions<ElectonContext> options)
            : base(options) { }

        #region UserTable

        public DbSet<User> Users { get; set; }
        #endregion


        #region Product Table
        public DbSet<ProductGroup> ProductGroups { get; set; }

        public DbSet<SubGroup> SubGroups { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductGallery> ProductGalleries { get; set; }

        #endregion

        #region OrderTable
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail>? orderDetails { get; set; } 
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}

using ElctroShop.Domain.Models;
using ElctroShop.Domain.Models.Order;
using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElctroShop.Infra.Data.Context
{
    public class ElctroShopContext : DbContext
    {
        public ElctroShopContext(DbContextOptions<ElctroShopContext> options)
              : base(options) { }

        #region UserTable
        public DbSet<User> Users { get; set; }
        #endregion


        #region ProductTable 
        public DbSet<ProductGroup> Groups { get; set; }

        public DbSet<ProductSubGroup> SubGroups { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductGallery> ProductGalleries { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        #endregion

        #region CommentTable
        public DbSet<Comment> Comments { get; set; }
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

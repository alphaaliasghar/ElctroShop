using ElctroShop.Domain.Interfaces;
using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.ViewModels;
using ElctroShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ElctroShopContext _context;

        public ProductRepository(ElctroShopContext context)
        {
            _context = context;
        }

        public int Count()
        {
            return _context.Products.Count();
        }

        public List<Product> GetAll(int take, int skip)
        {
            return _context.Products
                  .Include(p => p.ProductGroup)
                  .Include(p => p.ProductSubGroup)

                 .Skip(skip).Take(take).ToList();


        }

        public Product? GetProductById(int id)
        {
            return _context.Products
                .AsQueryable()
                .Include(p => p.ProductGroup)
                .Include(p => p.ProductSubGroup)
                .Include(p => p.ProductGalleries)
                .Include(p=>p.OrderDetails)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Product>? GetGroupByid(int groupId)
        {
            return _context.Products
                 .Include(p => p.ProductGroup)
                 .Include(p => p.ProductSubGroup)
                 .Include(p => p.ProductGalleries)
                 .Where(p => p.GroupId == groupId).ToList();


        }

        public List<Product>? GetSubGroupByid(int subgroupId)
        {
            return _context.Products
                   .Include(p => p.ProductGroup)
                 .Include(p => p.ProductSubGroup)
                 .Include(p => p.ProductGalleries)
                 .Where(p => p.SubGroupId == subgroupId)
               .ToList();
        }

        public void InsertProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public List<Product> SearchProduct(string q)
        {
            return _context.Products
                  .Where(p =>
                  p.Title.Contains(q) || p.ShortDescription.Contains(q) ||
                  p.Description.Contains(q) || p.Tags.Contains(q)).ToList();



        }
    }
}

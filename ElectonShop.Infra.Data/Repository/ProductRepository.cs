using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.Models.Product;
using ElectonShop.Domain.Models.User;
using ElectonShop.Domain.ViewModels.Product;
using ElectonShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Infra.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ElectonContext _context;

        public ProductRepository(ElectonContext context)
        {
            _context = context;
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public List<ShowProductViewModel> GetAll(int take, int skip)
        {
            return _context.Products
                .Select(p => new ShowProductViewModel()
                {
                    Available = p.Available,
                    Bestseller = p.Bestseller,
                    Description = p.Description,
                    Group = p.ProductGroup,
                    ImageName = p.ImageName,
                    Popularproduct = p.Popularproduct,
                    Price = p.Price,
                    SubGroup = p.SubGroup,
                    Tags = p.Tags,
                    Title = p.Title,
                    CreateDate = p.CreateDate,
                    IsDelete = p.IsDelete,
                    ModifiDate = p.ModifiDate,
                    Id = p.Id,


                }).Skip(skip).Take(take).ToList();
        }

        public List<Product> GetAllBestseller()
        {
            return _context.Products
                      .Include(p => p.ProductGroup)
                   .Include(p => p.SubGroup)
                   .Include(p => p.ProductGalleries)
                   .Where(p => p.Bestseller && p.Bestseller).ToList();
        }

        public List<Product> GetAllPopularproduct( )
        {
            return _context.Products
                  .Include(p => p.ProductGroup)
                  .Include(p => p.SubGroup)
                  .Include(p => p.ProductGalleries)
                  .Where(p => p.Popularproduct&& p.Popularproduct).ToList();
        }

        public List<Product> GetGroupById(int groupId)
        {
            return _context.Products
                  .Include(p => p.ProductGroup)
                  .Include(p => p.SubGroup)
                  .Include(p => p.ProductGalleries)
                    .Where(p => p.GroupId == groupId && !p.IsDelete).ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products
                      .Include(p => p.ProductGroup)
                .Include(p => p.SubGroup)
                .Include(p => p.ProductGalleries)
                .Include(p=>p.OrderrDetails)
                   .FirstOrDefault(p => p.Id == id);
        }

        public List<ShowProductViewModel> GetSubGroupById(int subgroupId)
        {
            return _context.Products
                  .Include(p => p.SubGroup)
                   .Include(p => p.ProductGroup)
                  .Include(p => p.ProductGalleries)
                  .Where(p => p.SubGroupId == subgroupId)
                  .Select(p => new ShowProductViewModel()
                  {
                      Available = p.Available,
                      ImageName = p.ImageName,
                      Popularproduct = p.Popularproduct,
                      Price = p.Price,
                      Bestseller = p.Bestseller,
                      Description = p.Description,
                      Group = p.ProductGroup,
                      ProductGalleries = p.ProductGalleries,
                      SubGroup = p.SubGroup,
                      Tags = p.Tags,
                      Title = p.Title,
                      CreateDate = p.CreateDate,
                      Id = p.Id,
                      IsDelete = p.IsDelete,
                      ModifiDate = p.ModifiDate

                  }).ToList();

        }

        public void InsertProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public int PageCount()
        {
            return _context.Products.Count();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }
    }
}

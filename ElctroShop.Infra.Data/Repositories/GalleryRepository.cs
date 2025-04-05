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
    public class GalleryRepository : IGalleryRepository
    {
        private readonly ElctroShopContext _context;

        public GalleryRepository(ElctroShopContext context)
        {
            _context = context;
        }

        public void DeleteGallery(ProductGallery gallery)
        {
           _context.ProductGalleries.Remove(gallery);
        }

        public ProductGallery GetById(int id)
        {
            return _context.ProductGalleries
                .FirstOrDefault(g=>g.Id==id);
        }

        public List<GalleryViewModel> GetProductGallery(int productId)
        {
           return _context.ProductGalleries
                .Include(p=>p.Product)
                .Where(p=>p.ProductId==productId)
                .Select(p=>new GalleryViewModel()
                {
                    CreateDate = p.CreateDate,
                    Id = p.Id,
                    ImageName = p.ImageName,
                    IsDelete = p.IsDelete,
                    ModifiedDate=p.ModifiDate,
                    ProductId=p.ProductId,
                }).ToList();
        }

        public void InsertGallery(ProductGallery gallery)
        {
           _context.ProductGalleries.Add(gallery);
        }

        public void Save()
        {
         _context.SaveChanges();
        }

        public void UpdateGallery(ProductGallery gallery)
        {
           _context.ProductGalleries.Update(gallery);
        }
    }
}

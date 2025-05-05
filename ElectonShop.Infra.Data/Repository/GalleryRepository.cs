using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.Models.Product;
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
    public class GalleryRepository : IGalleryRepository
    {
   private readonly ElectonContext _context;

        public GalleryRepository(ElectonContext context)
        {
            _context = context;
        }

        public void DeleteGallery(ProductGallery gallery)
        {
           _context.ProductGalleries.Remove(gallery);
        }

        public ProductGallery? GetGalleryById(int Id)
        {
            return _context.ProductGalleries
                .FirstOrDefault(g => g.Id == Id);
        }

        public List<GalleryViewModel> GetProductgallery(int productId)
        {
          return _context.ProductGalleries
                .Include(g => g.Product)
                .Where(g => g.ProductId == productId)
                .Select(g=>new GalleryViewModel()
                {
                    Id = g.Id,
                    CreateDate =g.CreateDate,
                    ImageName =g.ImageName,
                    ModifiDate =g.ModifiDate,
                    ProductId=g.ProductId,
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

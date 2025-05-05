using ElectonShop.Application.Services.Interface;
using ElectonShop.Domain.Enums;
using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Application.Services.Implemation
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;

        public GalleryService(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        public ResultDeleteGallery DeleteGallery(int id)
        {
            var product = _galleryRepository.GetGalleryById(id);
            if (product == null)
                return ResultDeleteGallery.ProductGalleryNotFound;

            string deletepath = Guid.NewGuid().ToString()
               + Path.GetExtension(product.ImageName);
            string path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/ProductImage", deletepath);
            if (System.IO.File.Exists(deletepath))
            {
                System.IO.File.Delete(deletepath);
            }
            _galleryRepository.DeleteGallery(product);
            _galleryRepository.Save();
            return ResultDeleteGallery.Success;
        }

        public List<GalleryViewModel> GetProductgallery(int productId)
        {
            return _galleryRepository.GetProductgallery(productId);
        }
    }
}

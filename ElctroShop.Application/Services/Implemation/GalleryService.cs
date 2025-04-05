using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Domain.Enums;
using ElctroShop.Domain.Interfaces;
using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Application.Services.Implemation
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
            var gallery = _galleryRepository.GetById(id);
            if (gallery == null)
                return ResultDeleteGallery.ProductGalleryNotFount;

            string deleteimagename = Guid.NewGuid().ToString() +
                    Path.GetExtension(gallery.ImageName);
            if (System.IO.File.Exists(deleteimagename))
                System.IO.File.Delete(deleteimagename);

            _galleryRepository.DeleteGallery(gallery);
            _galleryRepository.Save();
            return ResultDeleteGallery.Success;
        }

        public List<GalleryViewModel> GetProductGalleries(int productId)
        {
            return _galleryRepository.GetProductGallery(productId);
        }
    }
}

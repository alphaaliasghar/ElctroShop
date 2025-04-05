using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.Interfaces
{
    public interface IGalleryRepository
    {
        void InsertGallery(ProductGallery gallery);

        void Save();

        void DeleteGallery(ProductGallery gallery);

        void UpdateGallery(ProductGallery gallery);

        List<GalleryViewModel> GetProductGallery(int productId);

        ProductGallery GetById(int id);


    }
}

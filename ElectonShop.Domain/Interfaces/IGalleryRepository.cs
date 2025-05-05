using ElectonShop.Domain.Models.Product;
using ElectonShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.Interfaces
{
    public interface IGalleryRepository
    {
        List<GalleryViewModel> GetProductgallery(int productId);

        void InsertGallery(ProductGallery gallery);

        void UpdateGallery(ProductGallery gallery);



        ProductGallery? GetGalleryById(int Id);
        void Save();

        void DeleteGallery(ProductGallery gallery);

    }
}

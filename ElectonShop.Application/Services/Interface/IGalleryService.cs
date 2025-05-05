using ElectonShop.Domain.Enums;
using ElectonShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Application.Services.Interface
{
    public interface IGalleryService
    {
        List<GalleryViewModel> GetProductgallery(int productId);

        ResultDeleteGallery DeleteGallery(int id);
    
    }
}

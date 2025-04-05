using ElctroShop.Domain.Enums;
using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Application.Services.Interfaces
{
    public interface IGalleryService
    {
        List<GalleryViewModel> GetProductGalleries(int productId);

        ResultDeleteGallery DeleteGallery(int id);

    }
}

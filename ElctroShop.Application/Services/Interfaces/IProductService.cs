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
    public interface IProductService
    {
        List<ProductViewModel> GetAll(int take, int skip);

        int PageCount();

      

        ProductViewModel GetForDetails(int id);

        ResultCreateProduct CreateProduct(CreateProductviewModel model);

        EditProductViewModel GetForEdit(int id);

        ResultEditProduct EditProduct(EditProductViewModel model);

        ResultDeleteProduct DeleteProduct(int id);

        void VisitProduct(int productId);

        List<ProductViewModel>? GetGroupById(int groupId);
        List<ProductViewModel>? GetSubGroupById(int subgroupId);

        Product? GetProductbyId(int id);


    }
}

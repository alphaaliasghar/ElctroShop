using ElectonShop.Domain.Enums;
using ElectonShop.Domain.Models.Product;
using ElectonShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Application.Services.Interface
{
    public interface IProductService
    {
        List<ShowProductViewModel> GetAll(int take, int skip);

        int PageCount();

        ResultCreateProduct CreateProduct(CreateProductViewModel model);


        ShowProductViewModel GetForDetails(int id);

        EditProductViewModel GetForEditProduct(int id);

        ResultEditProduct EditProduct(EditProductViewModel model);

        bool DeleteProduct(int id);

        List<ShowProductViewModel> GetGroupById(int id);

        List<ShowProductViewModel> GetSubGroupById(int subgroupId);

        List<ShowProductViewModel> GetAllPopularproduct();

        List<ShowProductViewModel> GetAllBestseller();

       
    }
}
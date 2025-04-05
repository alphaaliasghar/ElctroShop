using ElctroShop.Domain.Enums;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Application.Services.Interfaces
{
    public interface IProductGroupService
    {
        List<ProductGroupViewModel>? GetAll(int take, int skip);

        int Count();

        ResultCreateProductGroup Create(CreateProductGroupViewModel model);

        ProductGroupViewModel GetDetailById(int id);

        UpdateProductGroupViewModel GetForEdit(int id);

        ResultEditProductGroup EditProductGroup(UpdateProductGroupViewModel model);

      bool DeleteProductGroup(int id);

        List<ShowGroupViewModel> GetProductGroupForShow();


        List<SelectListViewModel> GetSelectedList();
    }
}

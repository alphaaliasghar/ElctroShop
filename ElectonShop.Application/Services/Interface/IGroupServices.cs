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
    public interface IGroupServices
    {
        List<ProductGroupViewModel> GetAll(int take, int skip);

        int PageCount();

        ProductGroupViewModel GetForDetailGroup(int id);

        ResultCreateProductGroup CreateProductGroup(CreateProductGroupViewModel model);

        UpdateProductGroupViewModel GetForEdit(int id);

        ResultEditProductGroup EditProductGroup(UpdateProductGroupViewModel model);

        bool DeleteProductgroup(int id);

        List<SelectListViewModel> GetBySelectList();
    

        List<ProductGroupForShow> ShowGroup();

  
    }
}

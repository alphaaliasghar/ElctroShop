using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElctroShop.Domain.Interfaces
{
    public interface IProductGroupRepository
    {
        List<ProductGroupViewModel>? GetAll(int take, int skip);

        int Count();

        void InsertGroup(ProductGroup productgroup);

        void save();

        ProductGroup? GetByProductGroupId(int id);

        void UpdateGroup(ProductGroup productgroup);

        List<ProductGroup> GetAllGroup();

        List<SelectListViewModel> GetSelectedList();

        bool Exist(int id);
    }
}

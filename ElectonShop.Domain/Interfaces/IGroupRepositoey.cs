using ElectonShop.Domain.Models.Product;
using ElectonShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.Interfaces
{
    public interface IGroupRepositoey
    {
        List<ProductGroupViewModel> GetAll(int take, int skip);

        int PageCount();

        void InsertGroup(ProductGroup productGroup);

        void UpdateGroup(ProductGroup productGroup);

        ProductGroup GetGroupById(int groupId);

        void Save();

        bool IsExist(int Id);

        List<SelectListViewModel> GetBySelectList();
      

        List<ProductGroup>GetAllGroup();
    }
}

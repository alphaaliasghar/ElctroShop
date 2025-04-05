using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.Interfaces
{
    public interface ISubGroupRepository
    {
        ProductSubGroup GetAllByGroupId(int groupId);
        void AddSubgroup(ProductSubGroup subgroup);

        void UpdateSubgroup(ProductSubGroup subgroup);

        void Save();

        ProductSubGroup GetSubGroupById(int groupId, int subgroupId);


        ProductSubGroup DeleteSubgroup(int subgroupId);

        List<SelectListViewModel> SelectedList(int? groupId = null);

        bool Exist(int id);

    }
}

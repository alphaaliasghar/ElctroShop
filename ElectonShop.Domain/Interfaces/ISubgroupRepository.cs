using ElectonShop.Domain.Models.Product;
using ElectonShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.Interfaces
{
    public interface ISubgroupRepository
    {
        void InsertSubgroup(SubGroup subgroup);

        void UpdateSubgroup(SubGroup subgroup);

        SubGroup? GetSubgroupByGroupId(int groupId,int subgroupId);

        void Save();

        bool IsExist(int Id);

        List<SelectListViewModel> GetBySelectList(int? groupId=null);
    }
}

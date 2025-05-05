using ElectonShop.Domain.Enums;
using ElectonShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Application.Services.Interface
{
    public interface ISubGroupService
    {
        ResultCreateSubGroup CreateSubGroup(CreateSubgroupViewModel model);

        UpdateSubgroupViewModel GetForEdit(int groupId, int subgroupId);


        ResultEditSubGroup UpdateSubGroup(UpdateSubgroupViewModel model);
  

        bool DeleteSubGroup(int groupId, int subgroupId);

        List<SelectListViewModel> GetBySelectlist(int? groupId=null);
    }
}

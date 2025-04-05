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
    public interface ISubGroupService
    {

        ResultCreateSubGroup CreateSubGroup(CreateSubgroupViewModel model);

        UpdateSubgroupViewModel GetForEdit(int groupId, int subGroupId);

        ResultEditSubGroup UpdateSubGroup(UpdateSubgroupViewModel model);

        bool DeleteSubGroup(int subgroupid);

        List<SelectListViewModel> SelectedList(int? groupId = null);
    }
}

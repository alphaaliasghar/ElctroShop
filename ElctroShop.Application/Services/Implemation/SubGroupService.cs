using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Domain.Enums;
using ElctroShop.Domain.Interfaces;
using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Application.Services.Implemation
{
    public class SubGroupService : ISubGroupService
    {
        private readonly ISubGroupRepository _subGroupRepository;

        public SubGroupService(ISubGroupRepository subGroupRepository)
        {
            _subGroupRepository = subGroupRepository;
        }

        public ResultCreateSubGroup CreateSubGroup(CreateSubgroupViewModel model)
        {
            ProductSubGroup subgroups = new ProductSubGroup()
            {
                CreateDate = DateTime.Now,
                SubGroupTitle = model.SubGroupTitle,
                GroupId = model.GroupId,
                IsDelete = false, 

            };
            _subGroupRepository.AddSubgroup(subgroups);
            _subGroupRepository.Save();
            return ResultCreateSubGroup.Success;
        }

        public bool DeleteSubGroup(int subgroupid)
        {
            var result = _subGroupRepository.DeleteSubgroup(subgroupid);
            if(result==null)
                return false;
            result.IsDelete = !result.IsDelete;
            _subGroupRepository.UpdateSubgroup(result);
            _subGroupRepository.Save();
            return true;
        }

       

        public UpdateSubgroupViewModel GetForEdit(int groupId, int subGroupId)
        {
            var subgroup = _subGroupRepository.GetSubGroupById(groupId,subGroupId);

            if (subgroup == null)
                return null;
            return new UpdateSubgroupViewModel()
            {
                GroupId = groupId,
                Id = subGroupId,
                SubGroupTitle = subgroup.SubGroupTitle,
            };
        }

        public List<SelectListViewModel> SelectedList(int? groupId = null)
        {
           return _subGroupRepository.SelectedList(groupId);
        }

        public ResultEditSubGroup UpdateSubGroup(UpdateSubgroupViewModel model)
        {
            var subgroup = _subGroupRepository.GetSubGroupById(model.GroupId,model.Id);

            if (subgroup == null)
                return ResultEditSubGroup.SubGroupNotFound;

            subgroup.SubGroupTitle = model.SubGroupTitle;
            subgroup.GroupId = model.GroupId;
            subgroup.Id = model.Id;

            _subGroupRepository.UpdateSubgroup(subgroup);
            _subGroupRepository.Save();
            return ResultEditSubGroup.Success;
        }
    }
}

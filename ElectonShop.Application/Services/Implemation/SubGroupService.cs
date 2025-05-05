using ElectonShop.Application.Services.Interface;
using ElectonShop.Domain.Enums;
using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.Models.Product;
using ElectonShop.Domain.ViewModels.Product;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Application.Services.Implemation
{
    public class SubGroupService : ISubGroupService
    {
        private readonly ISubgroupRepository _subgroupRepository;

        public SubGroupService(ISubgroupRepository subgroupRepository)
        {
            _subgroupRepository = subgroupRepository;
        }

        public ResultCreateSubGroup CreateSubGroup(CreateSubgroupViewModel model)
        {
            SubGroup subgroups = new SubGroup()
            {
                CreateDate = DateTime.Now,
                GroupId = model.GroupId,
                SubGroupTitle = model.SubgroupTitle,
            };
            _subgroupRepository.InsertSubgroup(subgroups);
            _subgroupRepository.Save();
            return ResultCreateSubGroup.Success;
        }

        public bool DeleteSubGroup(int groupId, int subgroupId)
        {
            var sub = _subgroupRepository.GetSubgroupByGroupId(groupId, subgroupId);
            if (sub == null)
                return false;
            sub.IsDelete = !sub.IsDelete;
            _subgroupRepository.UpdateSubgroup(sub);
            _subgroupRepository.Save();
            return true;
        }

        public List<SelectListViewModel> GetBySelectlist(int? groupId = null)
        {
           return _subgroupRepository.GetBySelectList(groupId);
        }

        public UpdateSubgroupViewModel GetForEdit(int groupId, int subgroupId)
        {
            var subgroup = _subgroupRepository.GetSubgroupByGroupId(groupId, subgroupId);
            if (subgroup == null)
            {
                return null;
            }
            return new UpdateSubgroupViewModel()
            {
                SubgroupTitle = subgroup.SubGroupTitle,
                GroupId = groupId,
                Id = subgroupId
            };
        }

        public ResultEditSubGroup UpdateSubGroup(UpdateSubgroupViewModel model)
        {
            var subgroup = _subgroupRepository.GetSubgroupByGroupId(model.GroupId, model.Id);
            if (subgroup == null)
            {
                return ResultEditSubGroup.SubGroupNotFound;
            }
            subgroup.SubGroupTitle = model.SubgroupTitle;
            subgroup.GroupId = model.GroupId;
            subgroup.Id = model.Id;
            _subgroupRepository.UpdateSubgroup(subgroup);
            _subgroupRepository.Save();
            return ResultEditSubGroup.Success;
        }
    }
}

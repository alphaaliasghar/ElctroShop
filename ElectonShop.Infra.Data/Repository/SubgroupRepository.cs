using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.Models.Product;
using ElectonShop.Domain.ViewModels.Product;
using ElectonShop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Infra.Data.Repository
{
    public class SubgroupRepository : ISubgroupRepository
    {
        private readonly ElectonContext _context;

        public SubgroupRepository(ElectonContext context)
        {
            _context = context;
        }

        public void InsertSubgroup(SubGroup subgroup)
        {
            _context.SubGroups.Add(subgroup);
        }

        public SubGroup? GetSubgroupByGroupId(int groupId, int subgroupId)
        {
            return _context.SubGroups
                .FirstOrDefault(s => s.GroupId == groupId && s.Id == subgroupId);
        }

        public void UpdateSubgroup(SubGroup subgroup)
        {
            _context.SubGroups.Update(subgroup);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool IsExist(int Id)
        {
            return _context.SubGroups
                 .Any(s => s.Id == Id);
        }

        public List<SelectListViewModel> GetBySelectList(int? groupId = null)
        {
            if (!groupId.HasValue)
            {
                return _context.SubGroups
                    .Select(s => new SelectListViewModel()
                    {
                        Id = s.Id,
                        Title = s.SubGroupTitle,
                    }).ToList();
            }
            else
            {
                return _context.SubGroups
                    .Where(s => s.GroupId == groupId)
                    .Select(s => new SelectListViewModel()
                    {
                        Id = s.Id,
                        Title = s.SubGroupTitle,
                    }).ToList();
            }
        }
    }
}

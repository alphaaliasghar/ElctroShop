using ElctroShop.Domain.Interfaces;
using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.ViewModels;
using ElctroShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Infra.Data.Repositories
{
    public class SubGroupRepository : ISubGroupRepository
    {
        private readonly ElctroShopContext _context;

        public SubGroupRepository(ElctroShopContext context)
        {
            _context = context;
        }

        public void AddSubgroup(ProductSubGroup subgroup)
        {
            _context.SubGroups.Add(subgroup);
        }

        public ProductSubGroup DeleteSubgroup(int subgroupId)
        {
            return _context.SubGroups
                  .FirstOrDefault(s => s.Id == subgroupId);
        }

        public bool Exist(int id)
        {
            return _context.SubGroups.Any(s => s.Id == id);
        }

        public ProductSubGroup GetAllByGroupId(int groupId)
        {
            return _context.SubGroups
                 .FirstOrDefault(s => s.GroupId == groupId);
        }

        public ProductSubGroup GetSubGroupById(int groupId, int subgroupId)
        {
            return _context.SubGroups.
                Include(s => s.Group).
                 FirstOrDefault(s => s.GroupId == groupId && s.Id == subgroupId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<SelectListViewModel> SelectedList(int? groupId = null)
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

        public void UpdateSubgroup(ProductSubGroup subgroup)
        {
            _context.SubGroups.Update(subgroup);
        }
    }
}

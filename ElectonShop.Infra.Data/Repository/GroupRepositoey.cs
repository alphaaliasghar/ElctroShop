using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.Models.Product;
using ElectonShop.Domain.ViewModels.Product;
using ElectonShop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Infra.Data.Repository
{
    public class GroupRepositoey:IGroupRepositoey
    {
      private readonly ElectonContext _context;

        public GroupRepositoey(ElectonContext context)
        {
            _context = context;
        }

        public List<ProductGroupViewModel> GetAll(int take, int skip)
        {
            return _context.ProductGroups
                .Include(p => p.SubGroups).ToList()
                 .Select(p => new ProductGroupViewModel()
                 {
                     CreateDate = p.CreateDate,
                     GroupTitle = p.GroupTitle,
                     Id = p.Id,
                     ModifiDate = p.ModifiDate,
                     GroupName = p.GroupName,
                     IsDelete = p.IsDelete,
                     SubGroups = p.SubGroups?.Select(s => new SubGroup()
                     {
                         GroupId = s.Id,
                         Id = s.Id,
                         CreateDate = s.CreateDate,
                         IsDelete = s.IsDelete,
                         ModifiDate = s.ModifiDate,
                         SubGroupTitle = s.SubGroupTitle,
                        
                     }).ToList()




                 }).Skip(skip).Take(take).ToList();
        }

        public List<ProductGroup> GetAllGroup()
        {
          return _context.ProductGroups
                .Include(_ => _.SubGroups)
                .Where(s=>!s.IsDelete).ToList();
        }

        public List<SelectListViewModel> GetBySelectList()
        {
           return _context.ProductGroups
                .Select(p => new SelectListViewModel()
                {
                    Id = p.Id,
                    Title=p.GroupTitle,
                }).ToList();
        }

        public ProductGroup GetGroupById(int groupId)
        {
            return _context.ProductGroups
                .Include(p=>p.SubGroups)
                .Include(p=>p.Products).
                 FirstOrDefault(p => p.Id == groupId);
        }

        public void InsertGroup(ProductGroup productGroup)
        {
          _context.ProductGroups.Add(productGroup);
        }

        public bool IsExist(int Id)
        {
           return _context.ProductGroups
                .Any(p => p.Id == Id);
        }

        public int PageCount()
        {
            return _context.ProductGroups.Count();
        }

        public void Save()
        {
          _context.SaveChanges();
        }

        public void UpdateGroup(ProductGroup productGroup)
        {
           _context.ProductGroups.Update(productGroup);
        }
    }
}

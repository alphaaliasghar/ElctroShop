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
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly ElctroShopContext _context;

        public ProductGroupRepository(ElctroShopContext context)
        {
            _context = context;
        }

        public int Count()
        {
            return _context.Groups.Count();
        }

        public bool Exist(int id)
        {
            return _context.Groups.Any(g => g.Id == id);
        }

        public List<ProductGroupViewModel>? GetAll(int take, int skip)
        {
            return _context.Groups
                 .Select(g => new ProductGroupViewModel()
                 {
                     Id = g.Id,
                     CreateDate = g.CreateDate,
                     GroupTitle = g.GroupTitle,
                     IsDelete = g.IsDelete,
                     ModifiDate = g.ModifiDate,
                     SubGroups = g.SubGroups.Select(s => new ProductSubGroup()
                     {
                         GroupId = s.Id,
                         CreateDate = s.CreateDate,
                         SubGroupTitle = s.SubGroupTitle,
                         Id = s.Id,
                         IsDelete = s.IsDelete,
                         ModifiDate = s.ModifiDate,

                     }).ToList()


                 }).Skip(skip).Take(take).ToList();
        }

        public List<ProductGroup> GetAllGroup()
        {
           return _context.Groups
                .Include(g => g.SubGroups)
                .Where(g=>!g.IsDelete).ToList();
        }

        public ProductGroup? GetByProductGroupId(int id)
        {
            return _context.Groups.Include(_g => _g.SubGroups)
                .FirstOrDefault(g => g.Id == id);
        }

        public List<SelectListViewModel> GetSelectedList()
        {
           return _context.Groups
             
                .Select(g=>new SelectListViewModel()
                {
                    Id = g.Id,
                    Title=g.GroupTitle,
                }).ToList();
        }

        public void InsertGroup(ProductGroup productgroup)
        {
            _context.Groups.Add(productgroup);
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public void UpdateGroup(ProductGroup productgroup)
        {
            _context.Groups.Update(productgroup);
        }



    }


}

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
    public class SearchRepository : ISearchRepository
    {
        private readonly ElctroShopContext _context;

        public SearchRepository(ElctroShopContext context)
        {
            _context = context;
        }

   

        public List<Product>? SearchProduct(string q)
        {
           return _context.Products
                .Include(p=>p.ProductGroup)
                .Where(p=>p.Title.Contains(q)||p.Description.Contains(q)||
                p.ShortDescription.Contains(q)).ToList();
        }
    }
}

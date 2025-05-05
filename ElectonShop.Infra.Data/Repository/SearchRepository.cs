using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.Models.Product;
using ElectonShop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Infra.Data.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ElectonContext _context;

        public SearchRepository(ElectonContext context)
        {
            _context = context;
        }

        public List<Product> Search(string q)
        {
          return _context.Products
                .Where(p=>p.Title.Contains(q)||
                p.Description.Contains(q)||p.Tags.Contains(q)).ToList();
        }
    }
}

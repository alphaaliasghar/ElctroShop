using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Domain.Interfaces;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Application.Services.Implemation
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public List<ProductViewModel>? SearchProduct(string q)
        {
            return _searchRepository.SearchProduct(q)?
                 .Select(s => new ProductViewModel()
                 {
                     CreateDate = s.CreateDate,
                     Description = s.Description,
                     ImageName = s.ImageName,
                     Id = s.Id,
                     ModifiDate = s.ModifiDate,
                     IsDelete = s.IsDelete,
                     Price = s.Price,
                     ShortDescription = s.ShortDescription,
                     Group = s.ProductGroup,
                     SubGroup = s.ProductSubGroup,
                     Tags = s.Tags,
                     Title = s.Title,
                     Visit = s.Visit,
                 }).ToList();
        }
    }
}

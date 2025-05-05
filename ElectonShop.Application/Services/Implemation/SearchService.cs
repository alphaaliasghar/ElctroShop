using ElectonShop.Application.Services.Interface;
using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Application.Services.Implemation
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public List<ShowProductViewModel> Search(string q)
        {
           return _searchRepository.Search(q)?
                .Select(s=>new ShowProductViewModel()
                {
                    Available = s.Available ,
                    Bestseller = s.Bestseller ,
                    CreateDate = s.CreateDate ,
                    Description = s.Description ,
                    Group=s.ProductGroup,
                    Id = s.Id ,
                    ImageName = s.ImageName ,
                    IsDelete = s.IsDelete ,
                    ModifiDate = s.ModifiDate ,
                    Popularproduct = s.Popularproduct ,
                    Price = s.Price ,
                    ProductGalleries   =s.ProductGalleries,
                    SubGroup =s.SubGroup ,
                    Tags =s.Tags ,
                    Title=s.Title

                }).ToList();
        }
    }
}

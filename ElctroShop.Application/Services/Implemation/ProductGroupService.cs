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

    public class ProductGroupService : IProductGroupService
    {

        private readonly IProductGroupRepository _productGroupRepository;

        public ProductGroupService(IProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }

        public int Count()
        {
            return _productGroupRepository.Count();
        }

        public ResultCreateProductGroup Create(CreateProductGroupViewModel model)
        {
            ProductGroup productGroups = new ProductGroup()
            {
                CreateDate = DateTime.Now,
                GroupTitle = model.GroupTitle,
                IsDelete = false,

            };
            _productGroupRepository.InsertGroup(productGroups);
            _productGroupRepository.save();
            return ResultCreateProductGroup.Success;
        }

        public bool DeleteProductGroup(int id)
        {
            var group = _productGroupRepository.GetByProductGroupId(id);

            if (group == null)
                return false;
            group.IsDelete = true;
            _productGroupRepository.UpdateGroup(group);
            _productGroupRepository.save();
            return true;
        }

        public ResultEditProductGroup EditProductGroup(UpdateProductGroupViewModel model)
        {
            var group = _productGroupRepository.GetByProductGroupId(model.Id);

            if (group == null)
            {
                return ResultEditProductGroup.ProductGroupNotFound;
            }
            group.GroupTitle = model.GroupTitle;
            group.IsDelete = model.IsDelete;
            group.ModifiDate = DateTime.Now;

            _productGroupRepository.UpdateGroup(group);
            _productGroupRepository.save();
            return ResultEditProductGroup.Success;

        }

        public List<ProductGroupViewModel>? GetAll(int take, int skip)
        {
            return _productGroupRepository.GetAll(take, skip);
        }

        public ProductGroupViewModel GetDetailById(int id)
        {
            var group = _productGroupRepository.GetByProductGroupId(id);
            if (group == null)
                return null;
            return new ProductGroupViewModel()
            {
                Id = id,
                CreateDate = group.CreateDate,
                GroupTitle = group.GroupTitle,
                IsDelete = group.IsDelete,
                ModifiDate = group.ModifiDate,


            };
        }

        public UpdateProductGroupViewModel GetForEdit(int id)
        {
            var group = _productGroupRepository.GetByProductGroupId(id);

            if (group == null)
                return null;
            return new UpdateProductGroupViewModel()
            {
                Id = id,
                GroupTitle = group.GroupTitle,
                IsDelete = group.IsDelete,
            };
        }
         
        public List<ShowGroupViewModel> GetProductGroupForShow()
        {
            var group = _productGroupRepository.GetAllGroup();
            return group.Select(g => new ShowGroupViewModel()
            {
                GroupTitle = g.GroupTitle,
                Id = g.Id,
                SubGroups = g.SubGroups?.ToList() ?? new List<ProductSubGroup>()
            }).ToList();
        }

        public List<SelectListViewModel> GetSelectedList()
        {
          return _productGroupRepository.GetSelectedList();
        }
    }
}

using ElectonShop.Application.Services.Interface;
using ElectonShop.Domain.Enums;
using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.Models.Product;
using ElectonShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Application.Services.Implemation
{
    public class GroupServices : IGroupServices
    {
        private readonly IGroupRepositoey _groupRepositoey;

        public GroupServices(IGroupRepositoey groupRepositoey)
        {
            _groupRepositoey = groupRepositoey;
        }

        public ResultCreateProductGroup CreateProductGroup(CreateProductGroupViewModel model)
        {
            ProductGroup groups = new ProductGroup()
            {
                GroupName = model.GroupName,
                CreateDate = DateTime.Now,
                GroupTitle = model.GroupTitle,

            };
            #region Manage GroupImage
            if (model.Image != null)
            {
                groups.GroupName = Guid.NewGuid().ToString()
                    + Path.GetExtension(model.Image.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Group", groups.GroupName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }

            }
            #endregion

            _groupRepositoey.InsertGroup(groups);
            _groupRepositoey.Save();
            return ResultCreateProductGroup.Success;
        }

        public bool DeleteProductgroup(int id)
        {
            var group = _groupRepositoey.GetGroupById(id);
            if (group == null)
            {
                return false;
            }
            group.IsDelete = !group.IsDelete;
            _groupRepositoey.UpdateGroup(group);
            _groupRepositoey.Save();
            return true;
        }

        public ResultEditProductGroup EditProductGroup(UpdateProductGroupViewModel model)
        {
            var group = _groupRepositoey.GetGroupById(model.Id);
            if (group == null)
                return ResultEditProductGroup.ProductGroupNotValid;

            group.GroupTitle = model.GroupTitle;
            group.IsDelete = model.IsDelete;
            group.ModifiDate = DateTime.Now;

            #region Manage Image Group
            if (model.Image != null)
            {
                if (group.GroupName != "noimage.jpg")
                {
                    string deletepath = Guid.NewGuid().ToString().ToString()
                        + Path.GetExtension(group.GroupName);
                    if (System.IO.File.Exists(deletepath))
                        System.IO.File.Delete(deletepath);

                }
                group.GroupName = Guid.NewGuid().ToString()
                    + Path.GetExtension(model.Image.FileName);

                string path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Group", group.GroupName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }
            }
            #endregion

            _groupRepositoey.UpdateGroup(group);
            _groupRepositoey.Save();
            return ResultEditProductGroup.Success;
        }

        public List<ProductGroupViewModel> GetAll(int take, int skip)
        {
            return _groupRepositoey.GetAll(take, skip);
        }

        public List<SelectListViewModel> GetBySelectList()
        {
            return _groupRepositoey.GetBySelectList();
        }

        public ProductGroupViewModel GetForDetailGroup(int id)
        {
            var group = _groupRepositoey.GetGroupById(id);
            if (group == null)
                return null;
            return new ProductGroupViewModel()
            {
                CreateDate = group.CreateDate,
                GroupName = group.GroupName,
                GroupTitle = group.GroupTitle,
                Id = id,
                IsDelete = group.IsDelete,
                ModifiDate = group.ModifiDate,
                SubGroups = group.SubGroups?.ToList() ?? new List<SubGroup>(),


            };
        }

        public UpdateProductGroupViewModel GetForEdit(int id)
        {
            var group = _groupRepositoey.GetGroupById(id);

            if (group == null)
                return null;

            return new UpdateProductGroupViewModel()
            {
                GroupTitle = group.GroupTitle,
                Id = id,
                IsDelete = group.IsDelete,
                ImageName = group.GroupName
            };
        }

 

        public int PageCount()
        {
            return _groupRepositoey.PageCount();
        }

        public List<ProductGroupForShow> ShowGroup()
        {
            var group = _groupRepositoey.GetAllGroup();
            return group.Select(g => new ProductGroupForShow()
            {
                ImageName = g.GroupName,
                GroupTitle= g.GroupTitle,
                Id = g.Id,
                SubGroups = g.SubGroups?.ToList() ?? new List<SubGroup>()
            }).ToList();
        }
    }
}

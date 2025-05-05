using ElectonShop.Application.Services.Interface;
using ElectonShop.Domain.Enums;
using ElectonShop.Domain.Interfaces;
using ElectonShop.Domain.Models.Product;
using ElectonShop.Domain.ViewModels.Product;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Application.Services.Implemation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IGroupRepositoey _groupRepositoey;

        private readonly ISubgroupRepository _subgroupRepository;

        private readonly IGalleryRepository _galleryRepository;

        public ProductService(IProductRepository productRepository,
            IGroupRepositoey groupRepositoey,
            ISubgroupRepository subgroupRepository,
            IGalleryRepository galleryRepository)
        {
            _productRepository = productRepository;
            _groupRepositoey = groupRepositoey;
            _subgroupRepository = subgroupRepository;
            _galleryRepository = galleryRepository;
        }

        public ResultCreateProduct CreateProduct(CreateProductViewModel model)
        {
            if (!_groupRepositoey.IsExist(model.GroupId))
            {
                return ResultCreateProduct.GroupNotValid;
            }
            if (!_subgroupRepository.IsExist(model.SubGroupId))
            {
                return ResultCreateProduct.SubGroupNotValid;
            }
            Product products = new Product()
            {
                Title = model.Title,
                Price = model.Price,
                IsDelete = false,
                SubGroupId = model.SubGroupId,
                Available = model.Available,
                Bestseller = model.Bestseller,
                CreateDate = DateTime.Now,
                Description = model.Description,
                GroupId = model.GroupId,
                ImageName = "noimage.jpg",
                Popularproduct = model.Popularproduct,
                Tags = model.Tags,




            };
            #region Manage ImageProduct
            if (model.Image != null)
            {
                products.ImageName = Guid.NewGuid().ToString()
                    + Path.GetExtension(model.Image.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/ProductImage", products.ImageName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }

            }
            #endregion

            _productRepository.InsertProduct(products);
            _productRepository.Save();

            #region Manage GalleryImage
            if (model.Galleries != null && model.Galleries.Any())
            {
                foreach (var img in model.Galleries)
                {
                    string imagename = Guid.NewGuid().ToString()
                        + Path.GetExtension(img.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/ProductImage", imagename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }
                    ProductGallery gallery = new ProductGallery()
                    {
                        CreateDate = DateTime.Now,
                        ImageName = imagename,
                        IsDelete = false,
                        ProductId = products.Id

                    };
                    _galleryRepository.InsertGallery(gallery);
                }
                _galleryRepository.Save();
            }
            #endregion

            return ResultCreateProduct.Success;
        }

        public bool DeleteProduct(int id)
        {
            var product = _productRepository.GetProductById(id);

            product.IsDelete = true;
            _productRepository.UpdateProduct(product);
            _productRepository.Save();
            return true;
        }

        public ResultEditProduct EditProduct(EditProductViewModel model)
        {
            var product = _productRepository.GetProductById(model.Id);
            if (product == null)
                return ResultEditProduct.ProductNotFound;
            if (!_groupRepositoey.IsExist(model.GroupId))
            {
                return ResultEditProduct.GroupNotFound;
            }
            if (!_subgroupRepository.IsExist(model.SubGroupId))
            {
                return ResultEditProduct.SubGroupNotFound;
            }


            product.Id = model.Id;
            product.Popularproduct = model.Popularproduct;
            product.ModifiDate = DateTime.Now;
            product.IsDelete = model.IsDelete;
            product.Available = model.Available;
            product.Bestseller = model.Bestseller;
            product.Description = model.Description;
            product.GroupId = model.GroupId;
            product.ImageName = model.ImageName;
            product.Price = model.Price;
            product.SubGroupId = model.SubGroupId;
            product.Tags = model.Tags;
            product.Title = model.Title;

            product.IsDelete = model.IsDelete;



            #region ManageImage
            if (model.Image != null)
            {
                if (product.ImageName != "noimage.jpg")
                {
                    product.ImageName = Guid.NewGuid().ToString()
                        + Path.GetExtension(model.Image.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory()
                        , "wwwroot/ProductImage", product.ImageName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        model.Image.CopyTo(stream);
                    }
                }
            }
            #endregion
            _productRepository.UpdateProduct(product);
            _productRepository.Save();

            #region ManageGallery
            if (model.Galleries != null && model.Galleries.Any())
            {
                foreach (var img in model.Galleries)
                {
                    string imagename = Guid.NewGuid().ToString()
                        + Path.GetExtension(img.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/ProductImage", imagename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }
                    ProductGallery gallery = new ProductGallery()
                    {
                        CreateDate = DateTime.Now,
                        ImageName = imagename,
                        IsDelete = false,
                        ProductId = product.Id,

                    };
                    _galleryRepository.InsertGallery(gallery);
                    _galleryRepository.Save();
                }

            }
            #endregion

            return ResultEditProduct.Success;
        }


        public List<ShowProductViewModel> GetAll(int take, int skip)
        {
            return _productRepository.GetAll(take, skip);
        }

        public List<ShowProductViewModel> GetAllBestseller()
        {
            var product = _productRepository.GetAllBestseller();
            return product.Select(p => new ShowProductViewModel()
            {
                Available = p.Available,
                Bestseller = p.Bestseller,
                CreateDate = p.CreateDate,
                Description = p.Description,
                Group = p.ProductGroup,
                Id = p.Id,
                ImageName = p.ImageName,
                IsDelete = p.IsDelete,
                ModifiDate = p.ModifiDate,
                Popularproduct = p.Popularproduct,
                Price = p.Price,
                ProductGalleries = p.ProductGalleries,
                SubGroup = p.SubGroup,
                Tags = p.Tags,
                Title = p.Title,
            }).ToList();
        }

        public List<ShowProductViewModel> GetAllPopularproduct()
        {
            var product = _productRepository.GetAllPopularproduct();
            return product.Select(p => new ShowProductViewModel()
            {
                Available = p.Available,
                Bestseller = p.Bestseller,
                CreateDate = p.CreateDate,
                Description = p.Description,
                Group = p.ProductGroup,
                Id = p.Id,
                ImageName = p.ImageName,
                IsDelete = p.IsDelete,
                ModifiDate = p.ModifiDate,
                Popularproduct = p.Popularproduct,
                Price = p.Price,
                ProductGalleries = p.ProductGalleries,
                SubGroup = p.SubGroup,
                Tags = p.Tags,
                Title = p.Title,
            }).ToList();
        }

        public ShowProductViewModel GetForDetails(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                Console.WriteLine($"Product with ID {id} not found.");
                return null;
            }

            var galleries = product.ProductGalleries?.Where(g => !g.IsDelete).ToList() ?? new List<ProductGallery>();

            return new ShowProductViewModel()
            {
                Available = product.Available,
                Bestseller = product.Bestseller,
                CreateDate = product.CreateDate,
                Description = product.Description,
                Group = product.ProductGroup,
                Id = id,
                ImageName = product.ImageName,
                IsDelete = product.IsDelete,
                ModifiDate = product.ModifiDate,
                Popularproduct = product.Popularproduct,
                Price = product.Price,
                ProductGalleries = galleries,

                SubGroup = product.SubGroup,
                Tags = product.Tags,
                Title = product.Title,



            };
        }

        public EditProductViewModel GetForEditProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                return null;

            return new EditProductViewModel()
            {
                Available = product.Available,
                Bestseller = product.Bestseller,
                Description = product.Description,

                Id = id,
                GroupId = product.GroupId,
                ImageName = product.ImageName,
                Popularproduct = product.Popularproduct,
                SubGroupId = product.SubGroupId,
                Price = product.Price,
                Tags = product.Tags,
                Title = product.Title,
                IsDelete = product.IsDelete,



            };
        }

        public List<ShowProductViewModel> GetGroupById(int id)
        {
            return _productRepository.GetGroupById(id)
                  .Select(s => new ShowProductViewModel()
                  {
                      IsDelete = s.IsDelete,
                      ImageName = s.ImageName,
                      Id = s.Id,
                      Group = s.ProductGroup,
                      Tags = s.Tags,
                      Title = s.Title,
                      Available = s.Available,
                      Bestseller = s.Bestseller,
                      Description = s.Description,
                      CreateDate = s.CreateDate,
                      ModifiDate = s.ModifiDate,
                      Popularproduct = s.Popularproduct,
                      Price = s.Price,
                      ProductGalleries = s.ProductGalleries,
                      SubGroup = s.SubGroup,


                  }).ToList();

        }


        public Product GetProductbyId(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public List<ShowProductViewModel> GetSubGroupById(int subgroupId)
        {
            return _productRepository.GetSubGroupById(subgroupId);
        }

        public int PageCount()
        {
            return _productRepository.PageCount();
        }
    }
}

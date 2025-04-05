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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IProductGroupRepository _productGroupRepository;

        private readonly ISubGroupRepository _subgroupRepository;

        private readonly IGalleryRepository _galleryRepository;

        public ProductService(IProductRepository productRepository,
            IProductGroupRepository productGroupRepository,
            ISubGroupRepository subgroupRepository,
            IGalleryRepository galleryRepository)
        {
            _productRepository = productRepository;
            _productGroupRepository = productGroupRepository;
            _subgroupRepository = subgroupRepository;
            _galleryRepository = galleryRepository;
        }




        public ResultCreateProduct CreateProduct(CreateProductviewModel model)
        {
            if (!_productGroupRepository.Exist(model.GroupId))
                return ResultCreateProduct.GroupNotValid;

            if (!_subgroupRepository.Exist(model.SubGroupId))
                return ResultCreateProduct.SubGroupNotFound;

            Product products = new Product()
            {
                CreateDate = DateTime.Now,
                GroupId = model.GroupId,
                SubGroupId = model.SubGroupId,
                ShortDescription = model.ShortDescription,
                Tags = model.Tags,
                Title = model.Title,
                Visit = 0,
                Price = model.Price,
                IsDelete = false,
                ImageName = "noimage.jpg",
                Description = model.Description,


            };

            #region Manage iImage
            if (model.Image != null)
            {
                products.ImageName = Guid.NewGuid().ToString() +
                    Path.GetExtension(model.Image.FileName);

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

            #region Manage Gallery

            if (model.Gallery != null && model.Gallery.Any())
            {
                foreach (var img in model.Gallery)
                {
                    string imagename = Guid.NewGuid().ToString() +
                                   Path.GetExtension(img.FileName);
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

        public ResultDeleteProduct DeleteProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                return ResultDeleteProduct.ProductNotFount;
            product.IsDelete = true;
            _productRepository.UpdateProduct(product);
            _productRepository.Save();

            return ResultDeleteProduct.Success;
        }

        public ResultEditProduct EditProduct(EditProductViewModel model)
        {
            var product = _productRepository.GetProductById(model.Id);
            if (product == null)
                return ResultEditProduct.ProductNotFound;
            if (!_productGroupRepository.Exist(model.GroupId))
                return ResultEditProduct.GroupNotFound;
            if (!_subgroupRepository.Exist(model.SubGroupId))
                return ResultEditProduct.SubGroupNotFound;

            product.SubGroupId = model.SubGroupId;
            product.Title = model.Title;
            product.GroupId = model.GroupId;
            product.ModifiDate = DateTime.Now;
            product.Description = model.Description;
            product.Visit = 0;
            product.ShortDescription = model.ShortDescription;
            product.Id = model.Id;
            product.IsDelete = model.IsDelete;


            #region Manage Image

            if (model.Image != null)
            {
                if (product.ImageName != "noimage.jpg")
                {
                    string deletepath = Guid.NewGuid().ToString() +
                        Path.GetExtension(product.ImageName);

                    if (System.IO.File.Exists(deletepath))
                    {
                        System.IO.File.Delete(deletepath);
                    }
                }
                product.ImageName = Guid.NewGuid().ToString() +
                    Path.GetExtension(model.Image.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/ProductImage", product.ImageName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }
            }
            #endregion

            _productRepository.UpdateProduct(product);
            _productRepository.Save();

            #region Manage Gallery


            if (model.Gallery != null && model.Gallery.Any())
            {
                foreach (var img in model.Gallery)
                {
                    string imagename = Guid.NewGuid().ToString() +
                  Path.GetExtension(img.FileName);
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
                }
                _galleryRepository.Save();
            }

            #endregion
            return ResultEditProduct.Success;
        }

        public List<ProductViewModel> GetAll(int take, int skip)
        {
            return _productRepository.GetAll(take,skip)
                .Select(p=>new ProductViewModel()
                {
                    CreateDate= p.CreateDate,
                    Description = p.Description,
                    Group=p.ProductGroup,
                    Id= p.Id,
                    ImageName= p.ImageName,
                    IsDelete= p.IsDelete,
                    ModifiDate= p.ModifiDate,
                    Price= p.Price,
                    ShortDescription= p.ShortDescription,
                    SubGroup=p.ProductSubGroup,
                    Tags= p.Tags,
                    Title= p.Title,
                    Visit=p.Visit,

                }).ToList();
        }

        public ProductViewModel GetForDetails(int id)
        {
            var product = _productRepository.GetProductById(id)
                ;
            if (product == null)
                return null;
            return new ProductViewModel()
            {
                CreateDate = product.CreateDate,
                Description = product.Description,
                ImageName = product.ImageName,
                IsDelete = product.IsDelete,
                Id = product.Id ,
                ModifiDate = product.ModifiDate,
                Group = product.ProductGroup,
                Price = product.Price,
                ProductGalleries = product.ProductGalleries,
                ShortDescription = product.ShortDescription,
                SubGroup = product.ProductSubGroup,
                Tags = product.Tags,
                Title = product.Title,
                Visit = product.Visit,
                GroupId=product.GroupId,
             
            };
        }

        public EditProductViewModel GetForEdit(int id)
        {
            var product = _productRepository.GetProductById(id);

            if (product == null)
                return null;

            return new EditProductViewModel()
            {
                Description = product.Description,
                GroupId = product.GroupId,
                Id = id,
                ImageName = product.ImageName,
                Price = product.Price,
                Galleries = product.ProductGalleries,
                ShortDescription = product.ShortDescription,
                Tags = product.Tags,
                Title = product.Title,
                SubGroupId = product.SubGroupId,
                IsDelete = product.IsDelete,


            };

        }



        public List<ProductViewModel>? GetGroupById(int groupId)
        {
            return _productRepository.GetGroupByid(groupId)
                .Select(g => new ProductViewModel()
                {
                    CreateDate = g.CreateDate,
                    Description = g.Description,
                    ImageName = g.ImageName,
                    IsDelete = g.IsDelete,
                    ModifiDate = g.ModifiDate,
                    Price = g.Price,
                    Tags = g.Tags,
                    Id = g.Id,
                    Title = g.Title,
                    Visit = g.Visit,
                    SubGroup = g.ProductSubGroup,
                    Group=g.ProductGroup,
                    ShortDescription=g.ShortDescription,
                }).ToList();
                
        }

        public Product? GetProductbyId(int id)
        {
          return _productRepository.GetProductById(id);
        }

        public List<ProductViewModel>? GetSubGroupById(int subgroupId)
        {
            return _productRepository.GetSubGroupByid(subgroupId)
                .Select(s=>new ProductViewModel()
                {
                    CreateDate = s.CreateDate,
                    ModifiDate= s.ModifiDate,
                    IsDelete= s.IsDelete,
                    ImageName= s.ImageName,
                    Id =s.Id,
                    Price= s.Price,
                    Description = s.Description,
                    Tags = s.Tags,
                    SubGroup= s.ProductSubGroup,
                    Group=s.ProductGroup,
                    Title= s.Title,
                    ShortDescription = s.ShortDescription,
                     Visit = s.Visit,
                }).ToList();
        }

        public int PageCount()
        {
            return _productRepository.Count();
        }

     

        public void VisitProduct(int productId)
        {
            var product = _productRepository.GetProductById(productId);

            if (product == null)
                return;
            product.Visit++;
            _productRepository.UpdateProduct(product);
            _productRepository.Save();
        }
    }
}

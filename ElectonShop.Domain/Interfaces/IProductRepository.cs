using ElectonShop.Domain.Models.Product;
using ElectonShop.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.Interfaces
{
    public interface IProductRepository 
    {
        List<ShowProductViewModel> GetAll(int take, int skip);

        void InsertProduct(Product product);

        List<Product> GetGroupById(int groupId);

        List<ShowProductViewModel> GetSubGroupById(int subgroupId);
        Product GetProductById(int id);

        void UpdateProduct(Product product);


        void DeleteProduct(Product product);

        void Save();

        int PageCount();

        List<Product> GetAllPopularproduct();

        List<Product> GetAllBestseller();

       

    }
}

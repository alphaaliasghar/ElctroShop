using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll(int take, int skip);

        int Count();


       


        void InsertProduct(Product product);

        void Save();

        Product? GetProductById(int id);

        void UpdateProduct(Product product);  
        
      

        List<Product>? GetGroupByid(int groupId);
        List<Product>? GetSubGroupByid(int subgroupId);

        List<Product> SearchProduct(string q);
    }
}

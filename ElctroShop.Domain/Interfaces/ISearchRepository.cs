using ElctroShop.Domain.Models.Product;
using ElctroShop.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.Domain.Interfaces
{
    public interface ISearchRepository
    {
        List<Product>? SearchProduct(string q);
      


    }
}

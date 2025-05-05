using ElectonShop.Domain.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.Domain.Interfaces
{
    public interface ISearchRepository
    {
        List<Product> Search(string q);
    }
}

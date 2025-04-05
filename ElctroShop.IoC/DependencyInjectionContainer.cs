using ElctroShop.Application.Services.Implemation;
using ElctroShop.Application.Services.Interfaces;
using ElctroShop.Domain.Interfaces;
using ElctroShop.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElctroShop.IoC
{
    public static class DependencyInjectionContainer
    {

        public static void RegisterServices(this IServiceCollection services)
        {
            #region Services

            services.AddScoped<IUserService,UserService>();
           
            services.AddScoped<IProductGroupService, ProductGroupService>();
          
            
            services.AddScoped<ISubGroupService, SubGroupService>();
           
            services.AddScoped<IProductService, ProductService>();
          
            services.AddScoped<IGalleryService, GalleryService>();
          
            services.AddScoped<ISearchService, SearchService>();
         
            services.AddScoped<IOrderService, OrderService>();
           
      
          
            
           
           
            #endregion

            #region Repository

            services.AddScoped<IUserRepository, UserRepository>();
           
            services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
          
            services.AddScoped<ISubGroupRepository, SubGroupRepository>();
          
            services.AddScoped<IProductRepository, ProductRepository>();
         
            services.AddScoped<IGalleryRepository, GalleryRepository>();
          
            services.AddScoped<ISearchRepository, SearchRepository>();
          
            services.AddScoped<IOrderRepository, OrderRepository>();
          
        
          
           
           
  

            #endregion
        }

    }
}

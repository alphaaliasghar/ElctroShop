using ElectonShop.Application.Services.Implemation;
using ElectonShop.Application.Services.Interface;
using ElectonShop.Domain.Interfaces;
using ElectonShop.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ElectonShop.IoC
{
    public static class DependencyInjectionContainer
    {
        public static void RegisterServices(this IServiceCollection Services)
        {
            #region Services

            Services.AddScoped<IUserService, UserService>();

            Services.AddScoped<IGroupServices, GroupServices>();

            Services.AddScoped<ISubGroupService, SubGroupService>();

            Services.AddScoped<IProductService, ProductService>();

            Services.AddScoped<IGalleryService, GalleryService>();

            Services.AddScoped<ISearchService, SearchService>();
           
            Services.AddScoped<IOrderService, OrderService>();




            #endregion

            #region Repository

            Services.AddScoped<IUserRepository, UserRepository>();

            Services.AddScoped<IGroupRepositoey, GroupRepositoey>();

            Services.AddScoped<ISubgroupRepository, SubgroupRepository>();


            Services.AddScoped<IProductRepository, ProductRepository>();

            Services.AddScoped<IGalleryRepository, GalleryRepository>();

            Services.AddScoped<ISearchRepository, SearchRepository>();
          
            Services.AddScoped<IOrderRepository, OrderRepository>();

            #endregion
        }
    }
}

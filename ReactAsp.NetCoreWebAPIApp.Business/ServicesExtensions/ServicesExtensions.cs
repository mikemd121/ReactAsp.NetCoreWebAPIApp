using ReactAsp.NetCoreWebAPIApp.Business.Implementations;
using ReactAsp.NetCoreWebAPIApp.Business.Interfaces;
using ReactAsp.NetCoreWebAPIApp.Repository.BaseRepository;
using ReactAsp.NetCoreWebAPIApp.Repository.BaseRepository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ReactAsp.NetCoreWebAPIApp.Business.ServicesExtensions
{
    public static class ServicesExtensions
    {

        /// <summary>
        /// Adds my library services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddMyLibraryServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<ISalesService, SalesService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}

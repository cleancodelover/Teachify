using FirstDigitCare.BL.BusinessLogic;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Teachify.DALS.Infrastructure.Interfaces;
using Teachify.BLL.BusinessLogic.Interfaces;
using Teachify.DALS.Infrastructure;
using Teachify.BL.BusinessLogic.Interfaces;
using Teachify.BLL.BusinessLogic;

namespace FirstDigitCare.BL.Infrastructure
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddBLLDependenciesLibraries(this IServiceCollection services)
        {

            services.AddEntityFrameworkSqlServer();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperWebProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILocationBL, LocationBL>();
            services.AddScoped<IProvidersBL, ProvidersBL>();
            services.AddScoped<IUsersBL, UsersBL>();
            services.AddScoped<ISetupBL, SetupBL>();
            services.AddScoped<ICustomerBL, CustomerBL>();
            services.AddScoped<IProvidersBL, ProvidersBL>();
            services.AddScoped<ICoursesBL, CoursesBL>();

            return services;
        }
    }
}

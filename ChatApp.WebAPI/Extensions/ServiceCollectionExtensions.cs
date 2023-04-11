using AutoMapper;
using ChatApp.BLL.Mapping;
using ChatApp.DAL.Data;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.Repositories.Realizations;
using ChatApp.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ChatApp.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<ChatAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AutoMapperProfile(AppDomain.CurrentDomain.GetAssemblies()));
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.SetPreflightMaxAge(TimeSpan.FromDays(1));
                });
            });

            services.AddLogging();
            services.AddControllers();
        }
        public static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApi", Version = "v1" });

                opt.CustomSchemaIds(x => x.FullName);
            });
        }
    }
}

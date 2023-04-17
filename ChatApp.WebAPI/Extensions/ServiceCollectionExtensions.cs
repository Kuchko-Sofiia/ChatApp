﻿using ChatApp.BLL.Mapping;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.BLL.Services;
using ChatApp.DAL.Data;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.Repositories.Realizations;
using ChatApp.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using ChatApp.DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ChatApp.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<ChatAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidIssuer = configuration["Jwt:Issuer"],

                        ValidateAudience = false,
                        ValidAudience = configuration["Jwt:Audience"],

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            services.AddAuthorization(options => options.DefaultPolicy =
                new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 8;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<ChatAppDbContext>()
                //.AddUserManager<User>()
                //.AddSignInManager<User>()
                .AddDefaultTokenProviders();

            //services.AddIdentityCore<User>()
            //    .AddEntityFrameworkStores<ChatAppDbContext>()
            //    .AddSignInManager<SignInManager<User>>();

            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AutoMapperProfile(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic).ToArray()));
            });

            //Repositories & Unit of Work
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();

            //Servises
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();
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
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatApp", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
                opt.CustomSchemaIds(x => x.FullName);
            });
        }
    }
}
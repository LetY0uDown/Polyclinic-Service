﻿using API_Host.Services;
using API_Host.Services.Interfaces;
using Database;
using Database.Models;
using Database.Repositories;
using Database.Services;
using HashidsNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace API_Host.Tools.Extensions;

/// <summary>
/// Набор методов-расширений для коллекции сервисов, используемых внутри API
/// </summary>
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddTools (this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IStringHasher, StringHasher>();
        services.AddTransient<JWTTokenGenerator>();
        services.AddSingleton<IHashids>(_ => new Hashids(config["Salt:IDs"], 8));
        services.AddSingleton<DTOConverter>();

        return services;
    }

    /// <summary>
    /// Добавляет в контейнер классы для работы с Базой Данных
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    internal static IServiceCollection AddDatabase (this IServiceCollection services)
    {
        services.AddDbContext<PolyclinicContext>();

        services.AddScoped<IRepository<Client>, ClientRepository>();
        services.AddScoped<IClientService, ClientService>();

        services.AddScoped<IRepository<Doctor>, DoctorRepository>();

        services.AddScoped<IRepository<Speciality>, SpecialityRepository>();

        services.AddScoped<IRepository<Schedule>, ScheduleRepository>();

        return services;
    }

    /// <summary>
    /// Добавляет в проект Swagger с возможностью использовать JWT авторизацию
    /// </summary>
    /// <param name="services"></param>
    /// <param name="useAuthorization"></param>
    /// <returns></returns>
    internal static IServiceCollection AddSwaggerGen (this IServiceCollection services, bool useAuthorization)
    {
        services.AddSwaggerGen(option => {
            if (!useAuthorization)
                return;

            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    /// <summary>
    /// Добавляет авторизацию с помощью JWT
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    internal static IServiceCollection AddJWTAuthentication (this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new() {
                        ValidateIssuer = true,
                        ValidIssuer = config["JWT:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = config["JWT:Audience"],

                        ValidateLifetime = true,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!))
                    };
                });

        services.AddAuthorization(options => options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                                                                            .RequireAuthenticatedUser()
        
        return services;
    }

    internal static IServiceCollection AddRouteConstraints (this IServiceCollection services)
    {
        services.AddRouting(options =>
            options.ConstraintMap.Add("hashid", typeof(HashIDConstraint)));

        return services;
    }
}
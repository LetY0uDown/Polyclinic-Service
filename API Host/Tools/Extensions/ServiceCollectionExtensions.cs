using API_Host.Models;
using API_Host.Services;
using API_Host.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace API_Host.Tools.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices (this IServiceCollection services)
    {
        services.AddDbContext<PolyclinicContext>();
        services.AddTransient<IStringHasher, StringHasher>();
        services.AddTransient<JWTTokenGenerator>();
        services.AddSingleton<IValidator<LoginData>, LoginDataValidator>();

        return services;
    }

    public static IServiceCollection AddSwaggerGenWithAuthorization (this IServiceCollection services)
    {
        services.AddSwaggerGen(option => {
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

    public static IServiceCollection AddJWTAuthentication (this IServiceCollection services, IConfiguration config)
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
                                                                            .Build());

        return services;
    }
}
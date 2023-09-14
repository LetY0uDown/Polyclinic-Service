using API_Host.Models;
using API_Host.Services;
using API_Host.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PolyclinicContext>();

builder.Services.AddTransient<IStringHasher, StringHasher>();
builder.Services.AddTransient<JWTTokenGenerator>();

builder.Services.AddSingleton<IValidator<LoginData>, LoginDataValidator>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new() {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:Audience"],

                        ValidateLifetime = true,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
                    };
                });

builder.Services.AddAuthorization(options => options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                                                                            .RequireAuthenticatedUser()
                                                                            .Build());

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
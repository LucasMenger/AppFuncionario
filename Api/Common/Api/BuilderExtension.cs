using System.Text;
using Api.Configurations;
using Api.Data;
using Api.Handlers;
using Api.Services;
using Core.Handlers;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Common.Api;

public static class BuilderExtension
{

    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        // builder.Services.AddDbContext<AppDbContext>(options =>
        //     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName);
        });
        
    }
    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        var jwtSettings = new JwtConfig();
        builder.Configuration.GetSection("JwtConfig").Bind(jwtSettings);

        if (string.IsNullOrEmpty(jwtSettings.Secret))
        {
            throw new ArgumentNullException("JwtConfig:Secret", "A chave secreta do JWT não pode ser nula ou vazia.");
        }

        builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

        var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
        builder.Services.AddControllers();
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                policy =>
                {
                    policy.WithOrigins("http://localhost:3000")  
                        .AllowAnyMethod()  
                        .AllowAnyHeader()  
                        .AllowCredentials();  
                });
        });
    }


    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();

        builder.Services.AddScoped<IEmployeeHandler, EmployeeHandler>();
        builder.Services.AddScoped<EmployeeService>();
        builder.Services.AddScoped<AuthService>();
    }
}
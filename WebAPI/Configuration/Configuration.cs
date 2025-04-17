using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Domain.Constants;
using WebAPI.Domain.Repositories;
using WebAPI.Data.Database;
using WebAPI.Application.Services;
using WebAPI.Services;

namespace WebAPI.Configuration;

public static class Configuration
{
    public static IServiceCollection InjectDataLayer(this IServiceCollection services)
    {
        services.AddDbContext<PracticeDbContext>();

        services.AddScoped<IUserRepository, UserDAO>();
        services.AddScoped<IStudentRepository, StudentDAO>();
        services.AddScoped<IRoleRepository, RoleDAO>();

        return services;
    }

    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    public static IServiceCollection InjectUtilities(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();

        return services;
    }

    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var bytes = Encoding.UTF8.GetBytes(config[Jwt.KeyPath]!);
                SymmetricSecurityKey key = new(bytes);

                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidAudience = config[Jwt.AudiencePath],
                    ValidateIssuer = true,
                    ValidIssuer = config[Jwt.IssuerPath],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateLifetime = true,
                };
            });

        return services;
    }
}

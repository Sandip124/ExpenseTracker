using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.Infrastructure.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection AddCookieAuthenticationConfiguration(this IServiceCollection services,string key)
        {
            services.AddAuthentication(
                    x =>
                    {
                        x.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        x.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    }
                ).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                    x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    }
                );

            services.AddAuthorization(
                options =>
                {
                    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                        JwtBearerDefaults.AuthenticationScheme,
                        CookieAuthenticationDefaults.AuthenticationScheme
                    );
                    defaultAuthorizationPolicyBuilder =
                        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
                }
            );
            return services;
        }
    }
}
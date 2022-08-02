using Domain.Models.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using VPMS.Api.PolicyRequirements.UserClaimRequirements;
using VPMS.Domain.Entities.IdentityUserEntities;
using VPMS.Persistence;
using VPMS.SharedKernel.Responses;

namespace VPMS.Api.DIServiceExtensions;

public static class IdentityConfig
{
    public static void AddIdentityConfig(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddIdentity<ApplicationUser, UserRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        })
          .AddEntityFrameworkStores<VPMSDbContext>()
          .AddDefaultTokenProviders();

        var jwtData = new JwtConfig();

        builder.Configuration.Bind(nameof(JwtConfig), jwtData);

        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
          .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = builder.Environment.IsDevelopment() ? false : true;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtData.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtData.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtData.SigningKey)),
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                };

                //This code came from https://www.blinkingcaret.com/2018/05/30/refresh-tokens-in-asp-net-core-web-api/
                //It returns a useful header if the JWT Token has expired

                options.Events = new JwtBearerEvents
                {
                    OnForbidden = async context =>
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse()
                        {
                            Errors = new List<KeyValuePair<string, IEnumerable<string>>>
                            {
                                new KeyValuePair<string, IEnumerable<string>>(nameof(HttpStatusCode.Forbidden),
                                new[] { "Access denied" })
                            }
                        }));
                    },

                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse()
                        {
                            Errors = new List<KeyValuePair<string, IEnumerable<string>>>
                            {
                                new KeyValuePair<string, IEnumerable<string>>(nameof(HttpStatusCode.Unauthorized),
                                new[] { context?.ErrorDescription ?? "Unauthenticated request" })
                            }
                        }));
                    }
                };
            });

        services.AddTransient<IAuthorizationHandler, UserClaimRequirementHandler>();

        services.AddAuthorization(options =>
        {
            AuthorizationPolicyConfigHelpers.ApplyPolicies(options);
        });
    }
}

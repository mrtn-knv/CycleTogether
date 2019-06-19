using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using DAL.Contracts;
using CycleTogether.Routes;
using CycleTogether.RoutesDifficultyManager;
using CycleTogether.RoutesSubscriber;
using CycleTogether.Contracts;
using CycleTogether.Equipments;
using DAL;
using Microsoft.AspNetCore.Http;
using CycleTogether.ImageManager;
using CycleTogether.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CycleTogether.Notifications;
using CycleTogether.Claims;
using CycleTogether.BindingModels;


namespace CycleTogetherWeb
{
        public static class Extensions
        {
            public static void SetupServices(this IServiceCollection services)
            {
                services.AddAutoMapper();
                services.AddScoped<IUserRepository, UsersRepository>();
                services.AddScoped<IRouteManager, RouteManager>();
                services.AddScoped<IRouteRepository, RoutesRepository>();
                services.AddScoped<IEquipmentsRepository, EquipmentRepository>();
                services.AddScoped<IImageRepository, PicturesRepository>();
                services.AddScoped<IUserRouteRepository, UserRoutesRepository>();
                services.AddScoped(typeof(TokenGenerator));
                services.AddScoped(typeof(ClaimsRetriever));
                services.AddScoped(typeof(DifficultyCalculator));
                services.AddScoped(typeof(Subscription));
                services.AddScoped<IGallery, CloudinaryStorage>();
                services.AddScoped<IAuthentication, Authenticator>();
                services.AddScoped<IEquipmentRetriever, EquipmentRetriever>();
                services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
                services.AddScoped<INotification, Notification>();
            }

            public static void AddAuthentication(this IServiceCollection services, AppSettings appSettings, byte[] key)
            {
                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                            .AddJwtBearer(x =>
                            {
                                x.RequireHttpsMetadata = false;
                                x.SaveToken = true;
                                x.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(key),
                                    ValidateIssuer = false,
                                    ValidateAudience = false
                                };
                            });
            }
        }
}

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
                services.AddSingleton<IUserRepository, UsersRepository>();
                services.AddSingleton<IRouteManager, RouteManager>();
                services.AddSingleton<IRouteRepository, RoutesRepository>();
                services.AddSingleton<IEquipmentsRepository, EquipmentRepository>();
                services.AddSingleton<IImageRepository, PicturesRepository>();
                services.AddSingleton(typeof(TokenGenerator));
                services.AddSingleton(typeof(ClaimsRetriever));
                services.AddSingleton(typeof(DifficultyCalculator));
                services.AddSingleton(typeof(Subscription));
                services.AddSingleton<IGallery, CloudinaryStorage>();
                services.AddSingleton<IAuthentication, Authenticator>();
                services.AddSingleton<IEquipmentRetriever, EquipmentRetriever>();
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddSingleton<INotification, Notification>();
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

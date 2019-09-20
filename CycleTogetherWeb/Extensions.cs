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
using CycleTogether.Cache;
using CycleTogether.DataRetriever;
using Microsoft.Extensions.Configuration;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using FluentValidation;
using WebModels;
using CycleTogether.Validation;
using Serilog;

namespace CycleTogetherWeb
{
    public static class Extensions
    {
        public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper();
            services.AddScoped<IUserRepository, UsersRepository>();
            services.AddScoped<IRouteManager, RouteManager>();
            services.AddScoped<IRouteRepository, RoutesRepository>();
            services.AddScoped<IEquipmentsRepository, EquipmentRepository>();
            services.AddScoped<IImageRepository, PicturesRepository>();
            services.AddScoped<IUserRouteRepository, UserRoutesRepository>();
            services.AddScoped<ISubscription, Subscription>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IClaimsRetriever, ClaimsRetriever>();
            services.AddScoped<IDifficultyCalculator, DifficultyCalculator>();
            services.AddScoped<IGallery, CloudinaryStorage>();
            services.AddScoped<IAuthentication, Authenticator>();
            services.AddScoped<IEquipmentRetriever, EquipmentRetriever>();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<INotification, Notification>();
            services.AddScoped<IUserEquipmentRepository, UserEquipmentsRepository>();
            services.AddScoped<IRouteEquipmentRepositoy, RouteEquipmentRepository>();
            services.AddSingleton<IRoutesCache, RoutesCache>();
            services.AddSingleton<IEquipmentCache, EquipmentsCache>();
            services.AddScoped<IDataRetriever, DataRetriever>();
            services.AddScoped<ISearchManager, SearchManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IValidator<WebModels.Route>, CycleTogether.Validation.Route>();
            services.AddSingleton<IValidator<WebModels.User>, CycleTogether.Validation.User>();

            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var mailCredentials = new NotificationCredentials();
            configuration.Bind("NotificationCredentials", mailCredentials);
            services.AddSingleton(mailCredentials);
            var cloudinary = new CloudinaryAccount();
            configuration.Bind("CloudinaryAccount", cloudinary);
            services.AddSingleton(cloudinary);
            var emails = new EmailProperties();
            configuration.Bind("EmailProperties", emails);
            services.AddSingleton(emails);


            services.AddDbContext<CycleTogetherDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(configuration["ConnectionStrings:DefaultConnectionString"],
                    b => b.MigrationsAssembly("DAL.Data")));

            var hfConnectionString = configuration["ConnectionStrings:HangFireConnectionString"];
            services.AddHangfire(h => h.UseSqlServerStorage(hfConnectionString));


            services.AddCors();
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(appSettings, key);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            })
            .AddFluentValidation();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel
                .Debug()
                .WriteTo
                .File("log.txt")
                .CreateLogger();

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

using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CycleTogether.BindingModels;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using Hangfire;

namespace CycleTogetherWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var mailCredentials = new NotificationCredentials();
            Configuration.Bind("NotificationCredentials", mailCredentials);
            services.AddSingleton(mailCredentials);
            var cloudinary = new CloudinaryAccount();
            Configuration.Bind("CloudinaryAccount", cloudinary);
            services.AddSingleton(cloudinary);
            var emails = new EmailProperties();
            Configuration.Bind("EmailProperties", emails);
            services.AddSingleton(emails);
            services.AddSingleton(RedisDatabase());

            services.AddDbContext<CycleTogetherDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(Configuration["ConnectionStrings:DefaultConnectionString"],
                    b => b.MigrationsAssembly("DAL.Data")));

            var hfConnectionString = Configuration["ConnectionStrings:HangFireConnectionString"];
            services.AddHangfire(h => h.UseSqlServerStorage(hfConnectionString));


            services.AddCors();
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(appSettings, key);
            services.SetupServices();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            app.UseAuthentication();

            app.UseMvc();
            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CycleTogetherDbContext>();
                context.Database.Migrate();
            }
        }

        private IDatabase RedisDatabase() => ConnectionMultiplexer.Connect("localhost").GetDatabase();
    }
}

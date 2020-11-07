using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullStack.API.Helpers;
using FullStack.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FullStack.Data;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //TODO: Add the DbContext and repositoy

            services.AddDbContext<FullStackDbContext>(options => options.UseSqlite("Data Source=Database.db"));
            services.AddScoped<IFullStackRepository, FullStackRepository>();
            services.AddScoped<FullStackDbContext>();
            services.AddTransient<AdvertService>();


            // configure DI for application services
            services.AddScoped<IUserService, UserService>();

            services.AddControllers();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
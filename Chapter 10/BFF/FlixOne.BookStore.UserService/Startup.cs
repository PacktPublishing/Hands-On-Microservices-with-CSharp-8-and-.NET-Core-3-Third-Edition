using FlixOne.BookStore.ProductService.Persistence;
using FlixOne.BookStore.UserService.Utility;
using FlixOne.BookStore.VendorService.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FlixOne.BookStore.UserService
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
            services.AddControllersWithViews();
            services.Configure<Settings>(options => {
                options.ValidIssuer
                        = Configuration.GetSection("JwtTokenConfig:ValidIssuer").Value;
                options.ValidAudience
                    = Configuration.GetSection("JwtTokenConfig:ValidAudience").Value;
                options.TokenExpiryTimeInMinutes
                    =  Configuration
                        .GetSection("JwtTokenConfig:TokenExpiryTimeInMinutes").Value.ToInt32();
                        
                options.JwtKey
                    = Configuration.GetSection("JwtTokenConfig:JwtKey").Value;
            });
            
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITokenUtility, TokenUtility>();
            services.AddDbContext<UserContext>(
                o => o.UseSqlServer(Configuration.GetConnectionString("UserDBConnection")));
            //Register Swagger
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "User APIs", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
                       
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSwagger();

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "User API V1");
            });
        }
    }
}

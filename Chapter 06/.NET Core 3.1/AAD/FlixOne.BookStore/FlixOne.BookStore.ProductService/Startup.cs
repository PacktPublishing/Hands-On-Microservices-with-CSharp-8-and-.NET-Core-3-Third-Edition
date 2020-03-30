using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using System;
using System.IO;
using System.Reflection;

namespace FlixOne.BookStore.ProductService
{
    public class Startup
    {
        public static string Impersonation;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adds Microsoft Identity platform (AAD v2.0) support to protect this Api
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddProtectedWebApi("AzureAd", Configuration, options => Configuration.Bind("AzureAD", options));
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(op =>
            {
                op.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Product API", Version = "v1" });
                op.DescribeAllParametersInCamelCase();
                //op.DescribeAllEnumsAsStrings();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                op.IncludeXmlComments(xmlPath);
                op.CustomSchemaIds(x => x.FullName);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            Impersonation = Configuration["AzureAd:impersonation"];

            app.UseHttpsRedirection();

            app.UseRouting();
             
            app.UseSwagger();

            app.UseSwaggerUI(op =>
            {
                op.SwaggerEndpoint("/swagger/v1/swagger.json", "Product APIs");
                op.RoutePrefix = string.Empty;
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

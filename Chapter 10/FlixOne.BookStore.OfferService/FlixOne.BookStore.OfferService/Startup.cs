using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FlixOne.BookStore.OfferService.Persistence;
using FlixOne.BookStore.OfferService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace FlixOne.BookStore.OfferService
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
            services.AddControllers();
            //Configure AppSettings
            services.AddOptions();
            services.Configure<AppSettings>(Configuration);
            //Register repository
            services.AddTransient<IOfferRepository, OfferRepository>();
            //HTTPClient services
            services.AddHttpClient<IDealService, DealService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));
            services.AddHttpClient<IVendorService, VendorService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddSwaggerGen(op =>
            {
                op.SwaggerDoc("v1", new OpenApiInfo { Title = "Offer API", Version = "v1" });
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
            app.UseSwagger();
            app.UseSwaggerUI(op =>
            {
                op.SwaggerEndpoint("/swagger/v1/swagger.json", "Offer APIs");
                op.RoutePrefix = string.Empty;
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

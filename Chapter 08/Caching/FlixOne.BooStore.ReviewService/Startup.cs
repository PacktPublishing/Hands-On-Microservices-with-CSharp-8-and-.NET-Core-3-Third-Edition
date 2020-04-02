using Alachisoft.NCache.Caching.Distributed;
using FlixOne.BooStore.ReviewService.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace FlixOne.BooStore.ReviewService
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
            //Redis Cache
            services.AddStackExchangeRedisCache(option =>
            {
                //option.Configuration = Configuration.GetConnectionString("CacheConnection");
                option.Configuration = "localhost";
                option.InstanceName = "FlixOneReview";
            });
            //sql
            services.AddDbContext<ReviewContext>(
                o => o.UseSqlServer(Configuration.GetConnectionString("ReviewDb"),
                sqlServerOptionsAction: sqlOptions =>
                {

                    //Resiliency
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }));
            //Redis sql-cache
           
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("ReviewDb");
                options.SchemaName = "dbo";
                options.TableName = "ReviewCache";
            });
            
            //Ncache
            /*
            services.AddNCacheDistributedCache(configuration =>
            {
                configuration.CacheName = "FlixOneReviewClusteredCache";
                configuration.EnableLogs = true;
                configuration.ExceptionsEnabled = true;
            });
            */
            //Register Swagger
            RegisterSwagger(services);
        }
        private static void RegisterSwagger(IServiceCollection services)
        {
            //Register Swagger
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Review APIs",
                    Version = "v1",
                    Description = "Api documentation of FlixOne Store Services",
                    TermsOfService = new Uri("https://github.com/PacktPublishing/Hands-On-Microservices-with-CSharp-8-and-.NET-Core-3-Third-Edition/blob/master/LICENSE"),
                    Contact = new OpenApiContact
                    {
                        Name = "Gaurav and Ed",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/PacktPublishing/Hands-On-Microservices-with-CSharp-8-and-.NET-Core-3-Third-Edition/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://github.com/PacktPublishing/Hands-On-Microservices-with-CSharp-8-and-.NET-Core-3-Third-Edition/blob/master/LICENSE"),
                    }
                });

                // path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ReviewContext context )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            context.Database.EnsureCreated();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "Review API V1");
            });
        }
    }
}

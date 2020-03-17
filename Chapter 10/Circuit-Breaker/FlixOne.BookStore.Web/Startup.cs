using FlixOne.BookStore.Web.Config;
using FlixOne.BookStore.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace FlixOne.BookStore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddOptions();
            services.Configure<Settings>(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //client servcies
            services.AddHttpClient<IProductService, ProductService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(InstructToRetry())
                .AddPolicyHandler(WhatToDoIfServiceIsFaulty());

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        static private IAsyncPolicy<HttpResponseMessage> WhatToDoIfServiceIsFaulty()
        {
            //Setting Polly policy if remote service is faulty 
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));
        }

        static private IAsyncPolicy<HttpResponseMessage> InstructToRetry()
        {
            //Setting Polly policy to retry services
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(statusMessage => statusMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
              .WaitAndRetryAsync(3, howMuchRetryAttemps => TimeSpan.FromSeconds(Math.Pow(2, howMuchRetryAttemps)));
        }
    }
}

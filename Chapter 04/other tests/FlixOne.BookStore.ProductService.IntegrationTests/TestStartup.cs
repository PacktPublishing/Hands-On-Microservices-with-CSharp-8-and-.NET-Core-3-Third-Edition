using FlixOne.BookStore.ProductService.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlixOne.BookStore.ProductService.IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            //mock context
            services.AddDbContext<ProductContext>(
                o => o.UseSqlServer(Configuration.GetConnectionString("ProductConnection")));
            services.AddMvc();
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

        }

    }
}
using AutoMapper;
using FlixOne.BookStore.UserService.Extensions;
using FlixOne.BookStore.UserService.Helper;
using FlixOne.BookStore.UserService.Models.Identity;
using FlixOne.BookStore.UserService.Utility;
using FlixOne.BookStore.VendorService.Contexts;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Net;
using System.Reflection;
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
            //Add EF service
            services.AddDbContext<UserContext>(
                o => o.UseSqlServer(Configuration.GetConnectionString("UserDBConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetType().Assembly.GetName().Name);
                    //Resiliency
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }));

            services.AddTransient<ITokenUtility, TokenUtility>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            WireUpJWT(services);
            DefinePolicyForUser(services);
            BuildIdentity(services);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            RegisterSwagger(services);
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            //Register Swagger
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                { 
                    Title = "User APIs", 
                    Version = "v1",
                    Description = "Api documentation of FlixOne Identity server",
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
        }

        private static void BuildIdentity(IServiceCollection services)
        {
            var identityBuilder = services.AddIdentityCore<AppUser>(identityOption =>
            {
                identityOption.Password.RequireDigit = false;
                identityOption.Password.RequireLowercase = false;
                identityOption.Password.RequireUppercase = false;
                identityOption.Password.RequireNonAlphanumeric = false;
                identityOption.Password.RequiredLength = 6;
            });
            identityBuilder = new Microsoft.AspNetCore.Identity.IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<UserContext>().AddDefaultTokenProviders();
        }

        private static void DefinePolicyForUser(IServiceCollection services)
        {
            services.AddAuthorization(config => config.AddPolicy("FlixOneUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.FlixOneApiAccess)));

            services.AddAuthorization(config => config.AddPolicy("FlixOneAdmin", policy => policy.RequireRole("Admin")));
        }

        private void WireUpJWT(IServiceCollection services)
        {
            var jwtConfig = Configuration.GetSection("JwtTokenConfig");
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig["JwtKey"]));
            services.Configure<JwtSettingOptions>(options =>
            {
                options.ValidIssuer = jwtConfig["ValidIssuer"];
                options.ValidAudience = jwtConfig["ValidAudience"];
                options.TokenExpiryTimeInMinutes = jwtConfig["TokenExpiryTimeInMinutes"].ToInt32();
                options.JwtKey = jwtConfig["JwtKey"];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParam = new TokenValidationParameters
            {

                ValidateIssuer = true,
                ValidIssuer = jwtConfig["ValidAudience"],

                ValidateAudience = true,
                ValidAudience = jwtConfig["ValidAudience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configOptions =>
            {
                configOptions.ClaimsIssuer = jwtConfig["ValidAudience"];
                configOptions.TokenValidationParameters = tokenValidationParam;
                configOptions.SaveToken = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserContext userContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseExceptionHandler(
              builder =>
              {
                  builder.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                                context.Response.AddApplicationError(error.Error.Message);
                                await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                            }
                        });
              });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseRouting();
            userContext.Database.EnsureCreated();
            //app.UseCors(x => x
            //   .AllowAnyOrigin()
            //   .AllowAnyMethod()
            //   .AllowAnyHeader());

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

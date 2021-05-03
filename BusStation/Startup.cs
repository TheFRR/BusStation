using BLL;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BusStation
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
            var connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<BaseContext>();

            services.AddDbContext<BaseContext>(options => options.UseSqlServer(connection));

            services.AddMvc().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

            services.AddScoped(typeof(IdentityDbContext<User>), typeof(BaseContext));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager =
            serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager =
            serviceProvider.GetRequiredService<UserManager<User>>();
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new
                IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            string adminEmail = "admin@mail.com";
            string adminPassword = "Aa123456!";
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };
                IdentityResult result = await
                userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            string userEmail = "user@mail.com";
            string userPassword = "Aa123456!";
            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                User user = new User
                {
                    Email = userEmail,
                    UserName = userEmail
                };
                IdentityResult result = await
                userManager.CreateAsync(user, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            CreateUserRoles(services).Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}

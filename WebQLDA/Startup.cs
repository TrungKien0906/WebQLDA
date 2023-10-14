using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebQLDA.DataObjects;
using MySql.EntityFrameworkCore.Extensions;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebQLDA.Models;

public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Đọc chuỗi kết nối từ tệp cấu hình
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Thêm dịch vụ Entity Framework Core sử dụng MySQL
            services.AddDbContext<MyDbContext>(options =>
            {
                options.UseMySQL(connectionString);
            });
        services.AddSession();
        services.AddControllersWithViews();
        services.AddDistributedMemoryCache();
    }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
           

        // Áp dụng chính sách CORS ở đây (nếu cần)

        app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "checkConnection",
                    pattern: "Database/CheckConnection",
                    defaults: new { controller = "Database", action = "CheckConnection" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");
                endpoints.MapControllerRoute(
                          name: "login",
                        pattern: "login",
                         defaults: new { controller = "Home", action = "Login" });

            });
        }

    }

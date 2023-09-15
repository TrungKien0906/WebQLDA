using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebQLDA.DataObjects;
using MySql.EntityFrameworkCore.Extensions;
using System.Configuration;

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
    
        // Thêm các dịch vụ khác tại đây nếu cần

        services.AddControllersWithViews();
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
        app.UseAuthorization();

        // Áp dụng chính sách CORS ở đây
        app.UseCors("AllowSpecificOrigin");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "checkConnection",
                pattern: "Database/CheckConnection",
                defaults: new { controller = "Database", action = "CheckConnection" });

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }

}

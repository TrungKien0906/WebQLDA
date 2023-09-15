using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebQLDA.DataObjects;

public class DatabaseController : Controller
{
    private readonly MyDbContext _context;

    public DatabaseController(MyDbContext context)
    {
        _context = context;
    }

    public IActionResult CheckConnection()
    {
        try
        {
            _context.Database.OpenConnection(); // Mở kết nối đến cơ sở dữ liệu
            ViewBag.Message = "Kết nối đến cơ sở dữ liệu MySQL thành công.";
        }
        catch (Exception ex)
        {
            ViewBag.Message = "Lỗi khi kết nối đến cơ sở dữ liệu MySQL: " + ex.Message;
        }
        finally
        {
            _context.Database.CloseConnection(); // Đảm bảo đóng kết nối sau khi sử dụng
        }

        return View();
    }
}

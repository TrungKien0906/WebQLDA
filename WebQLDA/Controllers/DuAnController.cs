using Microsoft.AspNetCore.Mvc;
using WebQLDA.DataObjects;
using WebQLDA.Models; // Thay thế "WebQLDA.Models" bằng namespace của model dự án của bạn

public class DuAnController : Controller
{
    private readonly MyDbContext _context; // Thay thế "ApplicationDbContext" bằng context của bạn

    public DuAnController(MyDbContext context)
    {
        _context = context;
    }

    // Action method để hiển thị form tạo dự án
    [HttpGet]
    public IActionResult CreateDuAn()
    {
        return View();
    }

    // Action method để xử lý việc tạo dự án khi POST form
    [HttpPost]
    public IActionResult CreateDuAn(DuAn duAn)
    {
        if (ModelState.IsValid)
        {
            // Thêm dự án vào cơ sở dữ liệu và lưu thay đổi
            _context.DuAn.Add(duAn);
            _context.SaveChanges();

            // Redirect hoặc thực hiện hành động khác sau khi tạo dự án thành công
            return RedirectToAction("Index"); // Chuyển hướng đến danh sách dự án, thay "Index" bằng tên action phù hợp
        }
        return View(duAn);
    }

    // Các action method khác cho việc hiển thị danh sách dự án, chi tiết dự án, sửa dự án, xóa dự án, vv.
}

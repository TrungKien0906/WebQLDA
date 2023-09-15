using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebQLDA.DataObjects;
using WebQLDA.Models;

namespace WebQLDA.Controllers
{
    public class CongViecController : Controller
    {
        private readonly MyDbContext _dbContext;

        public CongViecController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult CreateCongViec()
        {
            // Lấy danh sách dự án từ cơ sở dữ liệu
            var duAnList = _dbContext.DuAn.ToList();

            // Truyền danh sách dự án đến view
            ViewBag.duAn = duAnList;

            return View();
        }

        public IActionResult GetCongViec(int id)
        {
            // Lấy thông tin công việc từ cơ sở dữ liệu bằng id
            var congViec = _dbContext.CongViec.FirstOrDefault(c => c.MaCongViec == id);

            if (congViec == null)
            {
                return NotFound(); // Xử lý trường hợp công việc không tồn tại
            }

            var duAn = _dbContext.DuAn.FirstOrDefault(d => d.MaDuAn == congViec.MaDuAn);

            if (duAn != null)
            {
                // Đã lấy thông tin dự án thành công, bạn có thể truyền nó đến view để hiển thị thông tin dự án.
                ViewBag.DuAn = duAn; // Truyền thông tin dự án đến ViewBag

                return View(congViec); // Trả về view và truyền thông tin công việc và dự án
            }

            // Xử lý trường hợp không tìm thấy dự án liên quan
            return NotFound();
        }


        [HttpPost]
        public IActionResult CreateCongViec(CongViec congViec)
        {
            if (ModelState.IsValid)
            {
                // Thêm công việc vào cơ sở dữ liệu và lưu thay đổi
                _dbContext.CongViec.Add(congViec);
                _dbContext.SaveChanges();

                // Cập nhật dự án tương ứng bằng cách thêm công việc vào danh sách công việc của dự án
                var duAn = _dbContext.DuAn.FirstOrDefault(d => d.MaDuAn == congViec.MaDuAn);
                if (duAn != null)
                {
                    duAn.CongViec.Add(congViec);
                    _dbContext.SaveChanges();
                }

                // Chuyển hướng đến trang DanhSachCongViec của HomeController
                return RedirectToAction("DanhSachCongViec", "Home", new { id = congViec.MaDuAn });
            }
            return View(congViec);
        }


    }
}

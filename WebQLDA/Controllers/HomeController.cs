using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebQLDA.DataObjects;
using WebQLDA.Models;

namespace WebQLDA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var duAnList = _context.DuAn.Include(duAn => duAn.CongViec).ToList();
            ViewBag.DuAn = duAnList;
            return View(duAnList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult CreateDuAn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateDuAn([FromBody] DuAnViewModel duAnViewModel)
        {
            if (ModelState.IsValid)
            {
                // Chuyển đổi DuAnViewModel thành đối tượng DuAn
                var duAn = new DuAn
                {
                    TenDuAn = duAnViewModel.TenDuAn,
                    NgayBatDau = duAnViewModel.NgayBatDau,
                    NgayKetThuc = duAnViewModel.NgayKetThuc,
                    TrangThai = duAnViewModel.TrangThai
                    // Thêm các thuộc tính khác của DuAn nếu cần
                };

                _context.DuAn.Add(duAn);
                _context.SaveChanges();

                // Trả về một phản hồi JSON để thông báo kết quả
                return Json(new { success = true, message = "Dự án đã được tạo thành công." });
            }

            // Trả về lỗi nếu có lỗi xảy ra
            return Json(new { success = false, message = "Có lỗi xảy ra khi tạo dự án." });
        }

        [HttpGet]
        public IActionResult CreateCongViec()
        {
            return View();
        }

        public IActionResult DanhSachCongViec(int id)
        {
            // Lấy danh sách công việc của dự án có id tương ứng
            var congViecList = _context.CongViec.Where(cv => cv.MaDuAn == id).ToList();

            // Lấy thông tin dự án để hiển thị tiêu đề trang
            var duAn = _context.DuAn.FirstOrDefault(d => d.MaDuAn == id);

            if (duAn != null)
            {
                ViewBag.TieuDe = "Danh sách công việc của dự án: " + duAn.TenDuAn;
                return View(congViecList);
            }

            // Trong trường hợp không tìm thấy công việc hoặc dự án, bạn có thể xử lý thông báo hoặc chuyển hướng tùy ý.
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateCongViec([FromBody] CongViecViewModel congViecViewModel)
        {
            if (ModelState.IsValid)
            {
                // Chuyển đổi CongViecViewModel thành đối tượng CongViec và thêm vào cơ sở dữ liệu
                var congViec = new CongViec
                {
                    MaDuAn = congViecViewModel.MaDuAn,
                    TenCongViec = congViecViewModel.TenCongViec,
                    MoTa = congViecViewModel.MoTa,
                    NgayHetHan = congViecViewModel.NgayHetHan,
                    TrangThai = congViecViewModel.TrangThai
                };

                _context.CongViec.Add(congViec);
                _context.SaveChanges();

                // Trả về một phản hồi JSON để thông báo kết quả
                return Json(new { success = true, message = "Công việc đã được tạo thành công." });
            }

            // Trả về lỗi nếu có lỗi xảy ra
            return Json(new { success = false, message = "Có lỗi xảy ra khi tạo công việc." });
        }
    }
}

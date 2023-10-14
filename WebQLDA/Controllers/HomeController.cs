using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebQLDA.DataObjects;
using WebQLDA.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;
using System.Reflection.Metadata;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Security.Claims;

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

        public IActionResult BaoCao()
        {
            return View();
        }
        public IActionResult TrangCaNhan()
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





        [HttpPost]
        public async Task<IActionResult> DeleteDuAn(int id)
        {
            try
            {
                var duAn = await _context.DuAn.FindAsync(id);
                if (duAn == null)
                {
                    return Json(new { success = false, message = "Dự án không tồn tại." });
                }

                _context.DuAn.Remove(duAn);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Dự án đã được xóa thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi xóa dự án." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDuAn(int id)
        {
            try
            {
                var duAn = await _context.DuAn.FindAsync(id);
                if (duAn == null)
                {
                    return Json(new { success = false, message = "Dự án không tồn tại." });
                }

                return Json(new { success = true, data = duAn });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi tải dữ liệu dự án." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCongViec(int id)
        {
            try
            {
                var congViec = await _context.CongViec.FindAsync(id);
                if (congViec == null)
                {
                    return Json(new { success = false, message = "Công việc không tồn tại." });
                }

                return Json(new { success = true, data = congViec });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi tải thông tin công việc." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditDuAn([FromBody] DuAn duAn)
        {
            try
            {
                _context.Update(duAn);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Dự án đã được cập nhật thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật dự án." });
            }
        }
        [HttpGet]
        public IActionResult CreateCongViec()
        {
            return View();
        }

        public IActionResult DanhSachCongViec(int id)
        {
            // Lấy danh sách công việc của dự án có id tương ứng
            var congViecList = _context.CongViec .Where(cv => cv.MaDuAn == id).ToList();



            // Lấy thông tin dự án để hiển thị tiêu đề trang
            var duAn = _context.DuAn.FirstOrDefault(d => d.MaDuAn == id);

            if (duAn != null)
            {
                ViewBag.TieuDe = "Danh sách công việc của dự án: " + duAn.TenDuAn;
                var duAnList = _context.DuAn.ToList();
                ViewBag.DuAnList = duAnList;
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
                    TrangThai = congViecViewModel.TrangThai,
                    GhiChu = congViecViewModel.GhiChu
       
                };

                _context.CongViec.Add(congViec);
                _context.SaveChanges();

                // Trả về một phản hồi JSON để thông báo kết quả
                return Json(new { success = true, message = "Công việc đã được tạo thành công." });
            }

            // Trả về lỗi nếu có lỗi xảy ra
            return Json(new { success = false, message = "Có lỗi xảy ra khi tạo công việc." });
        }
     

        [HttpPost]
        public async Task<IActionResult> EditCongViec([FromBody] CongViec congViecData)
        {
            try
            {
                // Lấy công việc từ cơ sở dữ liệu dựa trên maCongViec
                var congViec = await _context.CongViec.FindAsync(congViecData.MaCongViec);
                if (congViec == null)
                {
                    return Json(new { success = false, message = "Công việc không tồn tại." });
                }

                // Cập nhật thông tin công việc với dữ liệu mới
                congViec.TenCongViec = congViecData.TenCongViec;
                congViec.MoTa = congViecData.MoTa;
                congViec.NgayHetHan = congViecData.NgayHetHan;
                congViec.TrangThai = congViecData.TrangThai;
                congViec.GhiChu = congViecData.GhiChu;

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.Update(congViec);
                await _context.SaveChangesAsync();

                // Trả về kết quả thành công
                return Json(new { success = true, message = "Cập nhật công việc thành công" });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về thông báo lỗi nếu có
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

  

        [HttpPost]
        public async Task<IActionResult> DeleteCongViec(int id)
        {
            try
            {
                var congViec = await _context.CongViec.FindAsync(id);
                if (congViec == null)
                {
                    return Json(new { success = false, message = "Công việc không tồn tại." });
                }

                _context.CongViec.Remove(congViec);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Công việc đã được xóa thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi xóa công việc." });
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string taiKhoan, string matKhau)
        {
            var user = _context.NguoiDung.SingleOrDefault(u => u.TaiKhoan == taiKhoan && u.MatKhau == matKhau);

            if (user != null)
            {
                // Đăng nhập thành công, bạn có thể lấy ID người dùng kiểu int từ đây
                int loggedInUserId = user.IdNguoiDung;

                // Tiếp tục xử lý và chuyển hướng
                // ...

                return RedirectToAction("Index");
            }
            else
            {
                // Đăng nhập không thành công, hiển thị thông báo lỗi
                ModelState.AddModelError(string.Empty, "Đăng nhập không thành công.");
                return View();
            }
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Trả về view đăng ký
        }

        [HttpPost]
        public IActionResult Register(NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem người dùng đã tồn tại hay chưa (có thể sử dụng cơ sở dữ liệu)
                var existingUser = _context.NguoiDung.SingleOrDefault(u => u.TaiKhoan == nguoiDung.TaiKhoan);

                if (existingUser == null)
                {
                    try
                    {
                        // Thêm người dùng vào cơ sở dữ liệu
                        _context.NguoiDung.Add(nguoiDung);
                        _context.SaveChanges();

                        // Chuyển hướng đến trang đăng nhập sau khi đăng ký thành công
                        return RedirectToAction("Login");
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi từ cơ sở dữ liệu và hiển thị thông báo lỗi
                        ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi thêm người dùng: " + ex.Message);
                    }
                }
                else
                {
                    // Người dùng đã tồn tại, hiển thị thông báo lỗi
                    ModelState.AddModelError(string.Empty, "Người dùng đã tồn tại.");
                }
            }

            // Nếu dữ liệu không hợp lệ hoặc có lỗi, trả về view đăng ký với thông báo lỗi
            return View(nguoiDung);
        }
        private byte[] GeneratePDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Tạo một tài liệu PDF mới
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 25, 25, 30, 30);
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms);
                document.Open();

                // Tạo font cho tiếng Việt
                BaseFont baseFont = BaseFont.CreateFont("c:\\windows\\fonts\\arialuni.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font vietnameseFont = new iTextSharp.text.Font(baseFont, 12);

                // Tạo tiêu đề
                iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Danh Sách Công Việc", vietnameseFont);
                title.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                document.Add(title);

                // Tạo bảng danh sách công việc
                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(7); // 7 cột cho Tên Công Việc, Mô Tả, Ngày Hết Hạn, Trạng Thái, Dự Án, Ghi Chú, Tên Dự Án

                // Thiết lập độ rộng của các cột
                float[] columnWidths = new float[] { 50f, 50f, 60f, 50f, 50f, 50f, 50f };
                table.SetWidths(columnWidths);

                // Thêm tiêu đề cột
                table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Tên Công Việc", vietnameseFont)));
                table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Mô Tả", vietnameseFont)));
                table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Ngày Hết Hạn", vietnameseFont)));
                table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Trạng Thái", vietnameseFont)));
                table.AddCell(new PdfPCell(new Phrase("Dự Án", vietnameseFont)));
                table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Ghi Chú", vietnameseFont)));
                table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("Tên Dự Án", vietnameseFont)));

                // Lấy danh sách công việc từ cơ sở dữ liệu
                var congViecList = _context.CongViec.Include(cv => cv.DuAn).ToList();

                // Thêm dữ liệu từ danh sách công việc vào bảng
                foreach (var congViec in congViecList)
                {
                    table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(congViec.TenCongViec, vietnameseFont)));
                    table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(congViec.MoTa, vietnameseFont)));
                    table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(congViec.NgayHetHan.ToShortDateString(), vietnameseFont)));
                    table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(congViec.TrangThai, vietnameseFont)));
                    table.AddCell(new PdfPCell(new Phrase(congViec.DuAn != null ? congViec.DuAn.TenDuAn : "Không có dự án liên kết", vietnameseFont)));
                    table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(!string.IsNullOrEmpty(congViec.GhiChu) ? congViec.GhiChu : "Không có ghi chú", vietnameseFont)));
                    table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(congViec.DuAn != null ? congViec.DuAn.TenDuAn : "", vietnameseFont)));
                }

                // Thêm bảng vào tài liệu PDF
                document.Add(table);
                document.Close();

                // Trả về dữ liệu PDF dưới dạng mảng byte[]
                return ms.ToArray();
            }
        }
        [HttpGet]
        public IActionResult SearchUsers(string searchUser)
        {
            if (!string.IsNullOrWhiteSpace(searchUser))
            {
                // Thực hiện tìm kiếm người dùng dựa trên searchUser
                var results = _context.NguoiDung
                    .Where(nguoidung => nguoidung.TaiKhoan.Contains(searchUser) || nguoidung.Ten.Contains(searchUser))
                    .Select(nguoidung => new { nguoidung.TaiKhoan, nguoidung.Ten })
                    .ToList();

                return Json(results);
            }
            else
            {
                // Nếu `searchUser` trống, trả về danh sách rỗng
                return Json(new List<object>());
            }
        }






        [HttpGet]
        public ActionResult TaoBaoCao()
        {
            try
            {
                byte[] pdfContent = GeneratePDF();
                return File(pdfContent, "application/pdf", "bao-cao-cong-viec.pdf");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi tạo báo cáo." });
            }
        }

    }


}


using System;
using System.ComponentModel.DataAnnotations;

namespace WebQLDA.Models
{
    public class ThanhVienDuAn
    {
        [Required]
        public int IdNguoiDung { get; set; }

        [Required]
        public int MaDuAn { get; set; }

        [Required]
        public string VaiTro { get; set; } // Đã sửa thành VARCHAR

        [Required]
        public string QuyenHan { get; set; } // Đã sửa thành VARCHAR

        public NguoiDung NguoiDung { get; set; }
        public DuAn DuAn { get; set; }

        [Key]
        public int ThanhVienDuAnId { get; set; } // Thêm khóa chính
    }
}

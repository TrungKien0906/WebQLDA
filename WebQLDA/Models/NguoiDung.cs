using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebQLDA.Models
{
    public class NguoiDung
    {
        [Key]
        [Required]
        public int IdNguoiDung { get; set; }

        [Required]
        public string TaiKhoan { get; set; }

        [Required]
        public string Ten { get; set; }

        [Required]
        public string Email { get; set; }

        public string SoDienThoai { get; set; } // Đã sửa thành VARCHAR

        [Required]
        public string MatKhau { get; set; }

        public List<ThanhVienDuAn> ThanhVienDuAn { get; set; }
    }
}

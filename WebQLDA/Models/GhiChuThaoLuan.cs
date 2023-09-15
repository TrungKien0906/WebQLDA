using System;
using System.ComponentModel.DataAnnotations;

namespace WebQLDA.Models
{
    public class GhiChuThaoLuan
    {
        [Required]
        public string NoiDung { get; set; }

        [Required]
        public int CongViecLienQuan { get; set; }

        [Required]
        public int NguoiTao { get; set; }

        [Required]
        public DateTime ThoiGianTao { get; set; }

        public CongViec CongViec { get; set; }
        public NguoiDung NguoiDung { get; set; }

        [Key]
        public int GhiChuThaoLuanId { get; set; } // Thêm khóa chính
    }
}

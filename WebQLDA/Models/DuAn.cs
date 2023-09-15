using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebQLDA.Models
{
    public class DuAn
    {
        [Key]
        [Required]
        public int MaDuAn { get; set; }

        [Required]
        public string TenDuAn { get; set; }

        [Required]
        public DateTime NgayBatDau { get; set; }

        [Required]
        public DateTime NgayKetThuc { get; set; }

        [Required]
        public string TrangThai { get; set; } // Đã sửa thành VARCHAR

        public List<CongViec> CongViec { get; set; } = new List<CongViec>();
        public List<ThanhVienDuAn> ThanhVienDuAn { get; set; } = new List<ThanhVienDuAn>();
    }
}

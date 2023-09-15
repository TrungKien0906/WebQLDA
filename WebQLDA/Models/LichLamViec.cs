using System;
using System.ComponentModel.DataAnnotations;

namespace WebQLDA.Models
{
    public class LichLamViec
    {
        [Key]
        public int LichLamViecId { get; set; } // Thêm khóa chính
        [Required]
        public DateTime NgayBatDau { get; set; }

        [Required]
        public DateTime NgayKetThuc { get; set; }

        [Required]
        public int CongViecLienQuan { get; set; }

        public TimeSpan? ThoiGianDuKienHoanThanh { get; set; }

        public CongViec CongViec { get; set; }

        
    }
}

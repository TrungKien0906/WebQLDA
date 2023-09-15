using System;
using System.ComponentModel.DataAnnotations;

namespace WebQLDA.Models
{
    public class DuAnViewModel
    {
        [Required]
        public string TenDuAn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayBatDau { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayKetThuc { get; set; }

        [Required]
        public string TrangThai { get; set; }

        // Các thuộc tính khác nếu cần
    }
}

    using System;
    using System.ComponentModel.DataAnnotations;

    namespace WebQLDA.Models
    {
        public class CongViec
        {
            [Key]
            [Required]
            public int MaCongViec { get; set; }

            [Required]
            public string TenCongViec { get; set; }

            public string MoTa { get; set; }

            [Required]
            public DateTime NgayHetHan { get; set; }

            [Required]
            public string TrangThai { get; set; } 

            [Required]
            public int MaDuAn { get; set; }

            public DuAn DuAn { get; set; }

            public string GhiChu { get; set; }

    }
    }

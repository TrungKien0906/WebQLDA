using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebQLDA.Models
{
    public class NguoiDung 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
   



    }
}

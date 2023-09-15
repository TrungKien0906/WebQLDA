using Microsoft.EntityFrameworkCore;
using WebQLDA.Models;

namespace WebQLDA.DataObjects
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<DuAn> DuAn { get; set; }
        public DbSet<CongViec> CongViec { get; set; }
        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<ThanhVienDuAn> ThanhVienDuAn { get; set; }
        public DbSet<LichLamViec> LichLamViec { get; set; }
        public DbSet<GhiChuThaoLuan> GhiChuThaoLuan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DuAn>()
                .HasMany(duan => duan.ThanhVienDuAn)
                .WithOne(tvda => tvda.DuAn)
                .HasForeignKey(tvda => tvda.MaDuAn);

            modelBuilder.Entity<CongViec>()
                .HasOne(c => c.DuAn)
                .WithMany(d => d.CongViec)
                .HasForeignKey(c => c.MaDuAn)
                .IsRequired();

            modelBuilder.Entity<ThanhVienDuAn>()
                .HasKey(tvda => new { tvda.IdNguoiDung, tvda.MaDuAn });

            modelBuilder.Entity<ThanhVienDuAn>()
                .HasOne(tvda => tvda.NguoiDung)
                .WithMany(nguoidung => nguoidung.ThanhVienDuAn)
                .HasForeignKey(tvda => tvda.IdNguoiDung);

            modelBuilder.Entity<ThanhVienDuAn>()
                .HasOne(tvda => tvda.DuAn)
                .WithMany(duan => duan.ThanhVienDuAn)
                .HasForeignKey(tvda => tvda.MaDuAn);
            
            modelBuilder.Entity<LichLamViec>()
                .HasKey(llv => new { llv.NgayBatDau, llv.CongViecLienQuan });

            modelBuilder.Entity<GhiChuThaoLuan>()
                .HasKey(gctl => new { gctl.NoiDung, gctl.CongViecLienQuan });
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public DbSet<LichLamViec> LichLamViec { get; set; }
        public DbSet<GhiChuThaoLuan> GhiChuThaoLuan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

 

            modelBuilder.Entity<CongViec>()
                .HasOne(c => c.DuAn)
                .WithMany(d => d.CongViec)
                .HasForeignKey(c => c.MaDuAn)
                .IsRequired();


            modelBuilder.Entity<LichLamViec>()
                .HasKey(llv => new { llv.NgayBatDau, llv.CongViecLienQuan });

          

        }
    }
}

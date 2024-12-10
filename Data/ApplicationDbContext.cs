using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using siu_smart_printing_service.Models;

namespace siu_smart_printing_service.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Buildings> Buildings { get; set; }
        public DbSet<Campus> Campus { get; set; }
        public DbSet<FileTypes> FileTypes { get; set; } 
        public DbSet<Printers> Printers { get; set; }
        public DbSet<PrintingLogs> PrintersLogs { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<UploadFile> UploadFiles { get; set; }
        public DbSet<Configurations> Configurations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Buildings>()
                .HasOne(p => p.campus)
                .WithMany(o => o.buildings)
                .HasForeignKey(p => p.campusId);

            builder.Entity<Rooms>()
                .HasOne(p => p.building)
                .WithMany(o => o.rooms)
                .HasForeignKey(p => p.buildingId);

            builder.Entity<Printers>()
                .HasOne(p => p.location)
                .WithMany(o => o.printers)
                .HasForeignKey(p => p.roomId);

            builder.Entity<UploadFile>()
                .HasOne(p => p.student)
                .WithMany(o => o.UploadFiles)
                .HasForeignKey(p => p.userId);

            builder.Entity<UploadFile>()
                .HasOne(p => p.fileTypes)
                .WithMany(o => o.files)
                .HasForeignKey(p => p.fileTypeId);

            builder.Entity<PrintingLogs>()
              .HasOne(pa => pa.printer)
              .WithMany(p => p.printingLogs)
              .HasForeignKey(pa => pa.printerId);


            builder.Entity<PrintingLogs>()
                .HasOne(pa => pa.uploadFile)
                .WithMany(u => u.printerLogs)
                .HasForeignKey(pa => pa.uploadFileId);
        }
    }
}

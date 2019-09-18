using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DocumentManagement.Models.Data
{
    public partial class DocumentManagementContext : DbContext
    {
        public DocumentManagementContext()
        {
        }

        public DocumentManagementContext(DbContextOptions<DocumentManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cabinet> Cabinet { get; set; }
        public virtual DbSet<DocumentLog> DocumentLog { get; set; }
        public virtual DbSet<F1> F1 { get; set; }
        public virtual DbSet<F2> F2 { get; set; }
        public virtual DbSet<F3> F3 { get; set; }
        public virtual DbSet<Favourite> Favourite { get; set; }
        public virtual DbSet<FileAuditLog> FileAuditLog { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<FrequentFolder> FrequentFolder { get; set; }
        public virtual DbSet<RecentFile> RecentFile { get; set; }
        public virtual DbSet<Recycle> Recycle { get; set; }
        public virtual DbSet<RoleList> RoleList { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }
        public virtual DbSet<UserInformation> UserInformation { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<UserLoginActivity> UserLoginActivity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=192.168.0.108;Database=DocumentManagement;User=sa;Password=12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {}
    }
}

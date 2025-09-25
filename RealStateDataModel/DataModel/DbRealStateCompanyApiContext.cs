using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateDataModel.DataModel
{
    public partial class DbRealStateCompanyApiContext : DbContext
    {
        public DbRealStateCompanyApiContext(DbContextOptions<DbRealStateCompanyApiContext> options)
        : base(options)
        {
        }

        public virtual DbSet<TbImageType> TbImageTypes { get; set; }

        public virtual DbSet<TbOwner> TbOwners { get; set; }

        public virtual DbSet<TbProperty> TbProperties { get; set; }

        public virtual DbSet<TbPropertyImage> TbPropertyImages { get; set; }

        public virtual DbSet<TbPropertyTrace> TbPropertyTraces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbImageType>(entity =>
            {
                entity.HasKey(e => e.IdImageType);

                entity.ToTable("tbImageType");

                entity.Property(e => e.ImageType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbOwner>(entity =>
            {
                entity.HasKey(e => e.IdOwner);

                entity.ToTable("tbOwner");

                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<TbProperty>(entity =>
            {
                entity.HasKey(e => e.IdProperty);

                entity.ToTable("tbProperty");

                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.CodeInternal).HasMaxLength(20);
                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.IdOwnerNavigation).WithMany(p => p.TbProperties)
                    .HasForeignKey(d => d.IdOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbProperty_tbProperty1");
            });

            modelBuilder.Entity<TbPropertyImage>(entity =>
            {
                entity.HasKey(e => e.IdPropertyImage);

                entity.ToTable("tbPropertyImage");

                entity.HasOne(d => d.IdImageTypeNavigation).WithMany(p => p.TbPropertyImages)
                    .HasForeignKey(d => d.IdImageType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbPropertyImage_tbImageType");

                entity.HasOne(d => d.IdPropertyNavigation).WithMany(p => p.TbPropertyImages)
                    .HasForeignKey(d => d.IdProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbPropertyImage_tbProperty");
            });

            modelBuilder.Entity<TbPropertyTrace>(entity =>
            {
                entity.HasKey(e => e.IdPropertyTrace);

                entity.ToTable("tbPropertyTrace");

                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.Tax).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.Value).HasColumnType("money");

                entity.HasOne(d => d.IdPropertyNavigation).WithMany(p => p.TbPropertyTraces)
                    .HasForeignKey(d => d.IdProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbPropertyTrace_tbProperty");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

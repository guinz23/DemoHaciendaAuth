using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HaciendaEndPoints.Models
{
    public partial class HACIENDAContext : DbContext
    {
        public HACIENDAContext()
        {
        }

        public HACIENDAContext(DbContextOptions<HACIENDAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<VoucherResponse> VoucherResponse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=HACIENDA;User ID=sa;Password=reallyStrongPwd123;Connection Timeout=9000;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Token>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .HasColumnName("access_token");

                entity.Property(e => e.ExpiresIn)
                    .IsRequired()
                    .HasColumnName("expires_in");

                entity.Property(e => e.RefreshExpiresIn)
                    .IsRequired()
                    .HasColumnName("refresh_expires_in");

                entity.Property(e => e.RefreshToken)
                    .IsRequired()
                    .HasColumnName("refresh_token");

                entity.Property(e => e.SessionState)
                    .IsRequired()
                    .HasColumnName("session_state");

                entity.Property(e => e.TokenType)
                    .IsRequired()
                    .HasColumnName("token_type");
            });

            modelBuilder.Entity<VoucherResponse>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Clave).IsRequired();

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IntEstado).IsRequired();

                entity.Property(e => e.RespuestaXml).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

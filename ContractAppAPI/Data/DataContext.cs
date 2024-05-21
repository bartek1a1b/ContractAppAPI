using ContractAppAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace ContractAppAPI.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, 
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<ContractAppAPI.Models.Contract> Contracts { get; set; }
        public DbSet<ContractTypeOne> ContractTypeOnes { get; set; }
        public DbSet<ContractTypeTwo> ContractTypeTwos { get; set; }
        public DbSet<AnnexToTheContract> AnnexToTheContracts { get; set; }
        public DbSet<ContractPdf> ContractPdfs { get; set; }
        public DbSet<Pdf> Pdfs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            modelBuilder.Entity<ContractTypeOne>()
                .HasMany(cto => cto.ContractTypeTwos)
                .WithOne(ctt => ctt.ContractTypeOne)
                .HasForeignKey(ctt => ctt.ContractTypeOneId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ContractAppAPI.Models.Contract>()
                .HasOne(c => c.ContractTypeOne)
                .WithMany(cto => cto.Contracts)
                .HasForeignKey(c => c.ContractTypeOneId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContractAppAPI.Models.Contract>()
                .HasOne(c => c.ContractTypeTwo)
                .WithMany(ctt => ctt.Contracts)
                .HasForeignKey(c => c.ContractTypeTwoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContractTypeTwo>()
                .HasOne(ctt => ctt.ContractTypeOne)
                .WithMany(cto => cto.ContractTypeTwos)
                .HasForeignKey(ctt => ctt.ContractTypeOneId);
        }
    }
}

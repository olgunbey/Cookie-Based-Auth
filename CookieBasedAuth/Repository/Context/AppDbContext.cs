using CookieBasedAuth.Entities;
using Microsoft.EntityFrameworkCore;

namespace CookieBasedAuth.Repository.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<UserClaims> UserClaims { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<RoleClaims> RoleClaims { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("***");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<UserClaims>().HasOne(x => x.User).WithMany(x => x.UserClaims).HasForeignKey(x => x.UserID);
            modelBuilder.Entity<Roles>().HasMany(x => x.UserRoles).WithOne(x => x.Roles).HasForeignKey(x => x.RoleID);
            modelBuilder.Entity<Users>().HasMany(x => x.UserRoles).WithOne(x => x.Users).HasForeignKey(x => x.UserID);
            modelBuilder.Entity<Roles>().HasMany(x => x.RoleClaims).WithOne(x => x.Role).HasForeignKey(x => x.RoleID);
        }
    }
}

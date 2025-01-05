using System;
using CodePulse.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace CodePulse.Data
{
    public class AuthDbContext : IdentityDbContext
    {

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // create reader and writer role 
            var readerRoleId = "9166b0b4-8b3a-4650-9be0-e417458c49f5";
            var writerRoleId = "ae94e494-f8d0-4cba-b4e6-8b413a6132e0";

            var roles = new List<IdentityRole>()
            {
                    new IdentityRole(){
                            Id= readerRoleId,
                            Name ="Reader",
                            NormalizedName ="Reader".ToUpper(),
                            ConcurrencyStamp = readerRoleId
                    },
                    new IdentityRole(){
                            Id= writerRoleId,
                            Name ="Writer",
                            NormalizedName ="Writer".ToUpper(),
                            ConcurrencyStamp = writerRoleId
                    },
            };
            builder.Entity<IdentityRole>().HasData(roles);

            // admin user 
            var adminUserId = "7dd160a8-4175-4e5e-b76e-c61383e81976";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper()


            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "admin@12345");
            builder.Entity<IdentityUser>().HasData(admin);
            // give roles to admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>(){
                    UserId = adminUserId,
                    RoleId = readerRoleId

                },
                 new IdentityUserRole<string>(){
                    UserId = adminUserId,
                    RoleId = writerRoleId

                },
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }



    }
}


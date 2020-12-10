using Dwagen.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dwagen.Model
{
    public class SeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            string RoleId = Guid.NewGuid().ToString();
            string UserId = Guid.NewGuid().ToString();

            // Adding SuperAdmin Role
            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
                {
                new IdentityRole {
                              Id = RoleId,
                              Name = "Admin",
                              NormalizedName = "Admin"
                          }
                 });
            // Adding First Super Admin User
            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = UserId,
                    UserName = "admin",
                    NormalizedUserName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "Admin"),
                }
            );
            // Adding User Profile For First Super Admin
            builder.Entity<UsersProfile>().HasData(
                new UsersProfile
                {
                    Id = Guid.NewGuid(),
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    NumberPhone = "01122544788",
                    Password = "12345"
                }
            );
            // Assign First Super Admin To SuperRole
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {

                    RoleId = RoleId,
                    UserId = UserId
                }
            );
        }
    }
}

using BackEnd.Core.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.EF
{
    public static class Seed
    {
       
    
            public static async Task SeedRoles(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
            {
                string[] roleNames = { "Admin", "Professor", "Student" };

                // 1. تأكد من وجود الـ Roles
                foreach (var role in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // 2. بيانات الأدمن الافتراضي
                var adminEmail = "Ahmed@admin.com";
                var adminUserName = adminEmail;
                var adminPassword = "Admin@123";
                 var roole = "Admin";
            // غيريها لو حبيتي

                // 3. تأكدي إذا كان الأدمن موجود
                var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

                if (existingAdmin == null)
                {
                    var admin = new AppUser
                    {
                        UserName = adminUserName,
                        Email = adminEmail,
                        EmailConfirmed = true,
                        Role= roole
                    };

                    var result = await userManager.CreateAsync(admin, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "Admin");
                    }
                    else
                    {
                        // لو فيه خطأ في الإنشاء
                        throw new Exception("Failed to create admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }
    }


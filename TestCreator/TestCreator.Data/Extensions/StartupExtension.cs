﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestCreator.Data.Database;
using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Extensions
{
    public static class StartupExtension
    {
        public static void AddIdentityFromData(this IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 8;
                })
            .AddEntityFrameworkStores<EfDbContext>();
        }

        public static void MigrateDataaseAndSeedInitialData(this IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EfDbContext>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                context.Database.Migrate();

                DbSeeder.Seed(context, roleManager, userManager);
            }
        }
    }
}
